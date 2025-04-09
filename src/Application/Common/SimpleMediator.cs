using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Common
{
    public class SimpleMediator : IMediator
    {
        private readonly Dictionary<Type, Func<object, object>> _queryHandlers = new();
        private readonly Dictionary<Type, Func<object, Task<object>>> _asyncQueryHandlers = new();
        private readonly Dictionary<Type, Action<object>> _commandHandlers = new();
        private readonly Dictionary<Type, Func<object, Task>> _asyncCommandHandlers = new();
        private readonly IServiceProvider _serviceProvider;

        public SimpleMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        #region Synchronous Registrations
        public void Register<TQuery, TResult>(Func<TQuery, TResult> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            _queryHandlers[typeof(TQuery)] = query => handler((TQuery)query) ??
                throw new InvalidOperationException($"Handler for {typeof(TQuery).Name} returned null");
        }

        public void Register<TCommand>(Action<TCommand> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            _commandHandlers[typeof(TCommand)] = command => handler((TCommand)command);
        }
        #endregion

        #region Asynchronous Registrations
        public void RegisterAsync<TQuery, TResult>(Func<TQuery, Task<TResult>> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            _asyncQueryHandlers[typeof(TQuery)] = async query =>
            {
                var result = await handler((TQuery)query);
                return result ?? throw new InvalidOperationException($"Async handler for {typeof(TQuery).Name} returned null");
            };
        }

        public void RegisterAsync<TCommand>(Func<TCommand, Task> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            _asyncCommandHandlers[typeof(TCommand)] = async command => await handler((TCommand)command);
        }
        #endregion

        #region Synchronous Dispatching
        public TResult Send<TQuery, TResult>(TQuery query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (!_queryHandlers.TryGetValue(typeof(TQuery), out var handler))
                throw new InvalidOperationException($"No handler registered for query {typeof(TQuery).Name}");

            var result = handler(query);
            return result is TResult typedResult ? typedResult :
                throw new InvalidOperationException($"Handler for {typeof(TQuery).Name} returned invalid result type");
        }

        public void Send<TCommand>(TCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (!_commandHandlers.TryGetValue(typeof(TCommand), out var handler))
                throw new InvalidOperationException($"No handler registered for command {typeof(TCommand).Name}");

            handler(command);
        }
        #endregion

        #region Asynchronous Dispatching
        public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (!_asyncQueryHandlers.TryGetValue(typeof(TQuery), out var handler))
                throw new InvalidOperationException($"No async handler registered for query {typeof(TQuery).Name}");

            var result = await handler(query);
            return result is TResult typedResult ? typedResult :
                throw new InvalidOperationException($"Async handler for {typeof(TQuery).Name} returned invalid result type");
        }

        public async Task SendAsync<TCommand>(TCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (!_asyncCommandHandlers.TryGetValue(typeof(TCommand), out var handler))
                throw new InvalidOperationException($"No async handler registered for command {typeof(TCommand).Name}");

            await handler(command);
        }
        #endregion

        #region Automatic Handler Registration
        public void RegisterHandlersFromAssembly(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            RegisterQueryHandlers(assembly);
            RegisterAsyncQueryHandlers(assembly);
            RegisterCommandHandlers(assembly);
            RegisterAsyncCommandHandlers(assembly);
        }

        private void RegisterQueryHandlers(Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract &&
                            t.GetInterfaces().Any(i => i.IsGenericType &&
                                                   i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)));

            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces().First(i => i.IsGenericType &&
                                                                      i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));
                var queryType = interfaceType.GetGenericArguments()[0];
                var resultType = interfaceType.GetGenericArguments()[1];

                var handlerInstance = _serviceProvider.GetService(handlerType);
                if (handlerInstance == null)
                {
                    handlerInstance = Activator.CreateInstance(handlerType) ??
                        throw new InvalidOperationException($"Failed to create instance of {handlerType.Name}");
                }

                var methodInfo = handlerType.GetMethod("Handle") ??
                    throw new InvalidOperationException($"Handler {handlerType.Name} does not have a Handle method");

                _queryHandlers[queryType] = query => methodInfo.Invoke(handlerInstance, new[] { query }) ??
                    throw new InvalidOperationException($"Handler {handlerType.Name}.Handle returned null");
            }
        }

        private void RegisterAsyncQueryHandlers(Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract &&
                            t.GetInterfaces().Any(i => i.IsGenericType &&
                                                   i.GetGenericTypeDefinition() == typeof(IAsyncQueryHandler<,>)));

            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces().First(i => i.IsGenericType &&
                                                                      i.GetGenericTypeDefinition() == typeof(IAsyncQueryHandler<,>));
                var queryType = interfaceType.GetGenericArguments()[0];
                var resultType = interfaceType.GetGenericArguments()[1];

                var handlerInstance = _serviceProvider.GetService(handlerType);
                if (handlerInstance == null)
                {
                    handlerInstance = Activator.CreateInstance(handlerType) ??
                        throw new InvalidOperationException($"Failed to create instance of {handlerType.Name}");
                }

                var methodInfo = handlerType.GetMethod("HandleAsync") ??
                    throw new InvalidOperationException($"Handler {handlerType.Name} does not have a HandleAsync method");

                _asyncQueryHandlers[queryType] = async query =>
                {
                    var task = (Task)methodInfo.Invoke(handlerInstance, new[] { query })! ??
                        throw new InvalidOperationException($"Handler {handlerType.Name}.HandleAsync returned null");
                    await task;

                    // Get the Result property using reflection since we don't know the exact Task<T> type at compile time
                    var resultProperty = task.GetType().GetProperty("Result") ??
                        throw new InvalidOperationException($"Could not get Result from Task returned by {handlerType.Name}.HandleAsync");

                    return resultProperty.GetValue(task) ??
                        throw new InvalidOperationException($"Result from {handlerType.Name}.HandleAsync was null");
                };
            }
        }

        private void RegisterCommandHandlers(Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract &&
                            t.GetInterfaces().Any(i => i.IsGenericType &&
                                                   i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));

            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces().First(i => i.IsGenericType &&
                                                                      i.GetGenericTypeDefinition() == typeof(ICommandHandler<>));
                var commandType = interfaceType.GetGenericArguments()[0];

                var handlerInstance = _serviceProvider.GetService(handlerType);
                if (handlerInstance == null)
                {
                    handlerInstance = Activator.CreateInstance(handlerType) ??
                        throw new InvalidOperationException($"Failed to create instance of {handlerType.Name}");
                }

                var methodInfo = handlerType.GetMethod("Handle") ??
                    throw new InvalidOperationException($"Handler {handlerType.Name} does not have a Handle method");

                _commandHandlers[commandType] = command => methodInfo.Invoke(handlerInstance, new[] { command });
            }
        }

        private void RegisterAsyncCommandHandlers(Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract &&
                            t.GetInterfaces().Any(i => i.IsGenericType &&
                                                   i.GetGenericTypeDefinition() == typeof(IAsyncCommandHandler<>)));

            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces().First(i => i.IsGenericType &&
                                                                      i.GetGenericTypeDefinition() == typeof(IAsyncCommandHandler<>));
                var commandType = interfaceType.GetGenericArguments()[0];

                var handlerInstance = _serviceProvider.GetService(handlerType);
                if (handlerInstance == null)
                {
                    handlerInstance = Activator.CreateInstance(handlerType) ??
                        throw new InvalidOperationException($"Failed to create instance of {handlerType.Name}");
                }

                var methodInfo = handlerType.GetMethod("HandleAsync") ??
                    throw new InvalidOperationException($"Handler {handlerType.Name} does not have a HandleAsync method");

                _asyncCommandHandlers[commandType] = async command =>
                {
                    var task = (Task)methodInfo.Invoke(handlerInstance, new[] { command })! ??
                        throw new InvalidOperationException($"Handler {handlerType.Name}.HandleAsync returned null");
                    await task;
                };
            }
        }
        #endregion
    }
}
