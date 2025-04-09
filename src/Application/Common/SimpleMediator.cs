using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Common;

namespace Application.Common
{
    public class SimpleMediator : IMediator
    {
        private readonly Dictionary<Type, Func<object, object>> _handlers = new();

        public void Register<TQuery, TResult>(Func<TQuery, TResult> handler)
        {
            _handlers[typeof(TQuery)] = query => handler((TQuery)query);
        }

        public TResult Send<TQuery, TResult>(TQuery query)
        {
            if (!_handlers.TryGetValue(typeof(TQuery), out var handler) || handler == null)
            {
                throw new InvalidOperationException($"No handler registered for {typeof(TQuery).Name}");
            }

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return (TResult)handler(query);
        }

        public void RegisterHandlersFromAssembly(Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)));

            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));
                var queryType = interfaceType.GetGenericArguments()[0];
                var resultType = interfaceType.GetGenericArguments()[1];

                var handlerInstance = Activator.CreateInstance(handlerType);
                var methodInfo = handlerType.GetMethod("Handle");

                _handlers[queryType] = query => methodInfo.Invoke(handlerInstance, new[] { query });
            }
        }
    }
}