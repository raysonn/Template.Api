using Template.Api.Middleware;
using Template.CrossCutting;
using Template.Domain.AutoMapper;
using Template.IoC;
using Template.RabbitMq;
using Uninter.Template.CrossCutting;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(conf => AutoMapperConfiguration.RegisterMappings(conf), AppDomain.CurrentDomain.GetAssemblies());

// Register dependencies using IoC container
NativeInjectorBootStrapper.RegisterServices(builder.Services);

// Configuration settings
ConnectionStrings.TemplateConnection = builder.Configuration.GetConnectionString("TemplateConnection");
foreach (var config in builder.Configuration.GetSection("AppConfiguration").GetChildren())
    Environment.SetEnvironmentVariable(config.Key, config.Value);
RabbitMqConfig.SetEnviromentVariablesApi();

builder.Services.Configure<AppSettingsObj>(builder.Configuration);
builder.Services.AddRefitClients(builder.Configuration);

var app = builder.Build();

// Set environment-specific middleware
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware configuration
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseResponseExceptionHandler();
app.MapControllers();
app.Run();