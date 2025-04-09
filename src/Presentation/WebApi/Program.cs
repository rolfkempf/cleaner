using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application.Common;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add application services
var mediator = new SimpleMediator();

// Register handlers from both WebApi and Application assemblies
mediator.RegisterHandlersFromAssembly(Assembly.GetExecutingAssembly());
mediator.RegisterHandlersFromAssembly(Assembly.Load("Application"));

builder.Services.AddSingleton<IMediator>(mediator);

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
