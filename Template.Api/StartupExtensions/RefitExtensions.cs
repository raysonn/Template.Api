using Refit;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Template.Domain.Interfaces.Api;

namespace Uninter.Template.CrossCutting
{
    public static class RefitExtensions
    {
        public static IServiceCollection AddRefitClients(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.Get<AppSettingsObj>();

            services.AddRefit<ITokenApi>(appSettings.TokenApi.Url);
            services.AddRefitSoap<ILabWareApi>(appSettings.LabWareApi.Url);

            return services;
        }

        private static IServiceCollection AddRefit<T>(this IServiceCollection services, string url, bool? connectionClose = null) where T : class
        {
            services
                .AddRefitClient<T>()
                .ConfigureHttpClient((options) =>
                {
                    options.BaseAddress = new Uri(url);
                    options.DefaultRequestHeaders.ConnectionClose = connectionClose;
                });

            return services;
        }
        
        private static IServiceCollection AddRefitSoap<T>(this IServiceCollection services, string url) where T : class
        {
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
            }).ConfigureHttpClient((options) => { options.BaseAddress = new Uri(url); });

            return services;
        }

        private static IServiceCollection AddRefitWithHandler<T1, THandler>(this IServiceCollection services, string url, bool? connectionClose = null)
            where T1 : class
            where THandler : DelegatingHandler
        {
            services
                .AddRefitClient<T1>()
                .ConfigureHttpClient((options) =>
                {
                    options.BaseAddress = new Uri(url);
                    options.DefaultRequestHeaders.ConnectionClose = connectionClose;
                })
                //.ConfigureHttpMessageHandlerBuilder(h => h.PrimaryHandler = AddCertificate(certificate.Path, certificate.Password))
                .AddHttpMessageHandler<THandler>();

            return services;
        }

        private static HttpClientHandler AddCertificate(string certificadoPath, string certificadoPassword)
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(
                new X509Certificate2
                (
                    certificadoPath,
                    certificadoPassword,
                    X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet
                ));

            return handler;
        }
    }
}