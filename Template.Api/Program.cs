using Refit;
using System.Xml;
using Template.Api.Middleware;
using Template.CrossCutting;
using Template.Domain.AutoMapper;
using Template.Domain.Interfaces.Api;
using Template.IoC;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(conf => AutoMapperConfiguration.RegisterMappings(conf), AppDomain.CurrentDomain.GetAssemblies());

SetServices(builder.Services);
NativeInjectorBootStrapper.RegisterServices(builder.Services);

var app = builder.Build();

SetConfig(app);
SetEnviroment(app);

void SetServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddRefitClient<ITokenApi>().ConfigureHttpClient((options) => { options.BaseAddress = new Uri(Environment.GetEnvironmentVariable("TokenApi")); });

    services.AddRefitClient<ILabWareApi>(new RefitSettings
    {
        ContentSerializer = new XmlContentSerializer(
            new XmlContentSerializerSettings
            {
                XmlReaderWriterSettings = new XmlReaderWriterSettings()
                {
                    ReaderSettings = new XmlReaderSettings { IgnoreWhitespace = true }
                }
            })
    }).ConfigureHttpClient((options) => { options.BaseAddress = new Uri("https://integrationqas.brf-corp.com"); });
}

void SetConfig(WebApplication app)
{
    ConnectionStrings.TemplateConnection = app.Configuration.GetConnectionString("TemplateConnection");
    app.Configuration.GetSection("AppConfiguration").GetChildren().ToList().ForEach(config => Environment.SetEnvironmentVariable(config.Key, config.Value));
}

void SetEnviroment(WebApplication app)
{
    if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseResponseExceptionHandler();
    app.MapControllers();
    app.Run();
}