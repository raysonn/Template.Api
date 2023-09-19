using Microsoft.OpenApi.Models;

namespace Uninter.Template.CrossCutting
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IWebHostEnvironment enviroment)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"Template API {enviroment.EnvironmentName}",
                    Version = "v1",
                    Description = "Template"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insira o Bearer Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                }); // Enable Bearer token o Swagger

                c.IncludeXmlComments(string.Format(@"{0}\Swagger.xml", AppDomain.CurrentDomain.BaseDirectory));

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            }); // Enable Swagger on Application on /Swagger

            return services;
        }
    }
}