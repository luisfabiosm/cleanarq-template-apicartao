using Adapters.RabbitMQ.Connection;
using FluentValidation;
using Infra.Register;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables()
                  .Build();

builder.Services.AddAPIExtensions(configuration);

var app = builder.Build();
app.UseAPIExtensions();
app.Run();
