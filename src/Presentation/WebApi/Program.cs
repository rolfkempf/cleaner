using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application.Common;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register all query and command handlers as services
Assembly applicationAssembly = Assembly.Load("Application");
var handlerTypes = applicationAssembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract &&
                (t.GetInterfaces().Any(i => i.IsGenericType &&
                    (i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>) ||
                     i.GetGenericTypeDefinition() == typeof(IAsyncQueryHandler<,>) ||
                     i.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                     i.GetGenericTypeDefinition() == typeof(IAsyncCommandHandler<>)))));

foreach (var handlerType in handlerTypes)
{
    builder.Services.AddTransient(handlerType);
}

// Register the mediator as a singleton with dependency injection
builder.Services.AddSingleton<IMediator>(serviceProvider =>
{
    var mediator = new SimpleMediator(serviceProvider);

    // Register handlers from both WebApi and Application assemblies
    mediator.RegisterHandlersFromAssembly(Assembly.GetExecutingAssembly());
    mediator.RegisterHandlersFromAssembly(applicationAssembly);

    return mediator;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Swagger/NSwag configuration removed
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
