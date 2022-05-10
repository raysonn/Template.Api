using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Reflection;
using Template.Domain.Handlers;
using Template.Infra.Managers;
using Template.Service.Services;

namespace Template.IoC
{
    public class NativeInjectorBootStrapper
    {
        private const string NAMESPACEBASE = "Template";

        public static void RegisterServices(IServiceCollection services)
        {
            var Assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.ManifestModule.Name.Contains(NAMESPACEBASE)).ToList();

            Assemblies.Add(typeof(BaseService).Assembly);

            AutoInjector(services, Assemblies, "Infra");
            AutoInjector(services, Assemblies, "Service");

            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<TemplateConnectionManager>();
            services.AddTransient<TokenApiHeaderHandler>();
        }

        private static void AutoInjector(IServiceCollection services, IEnumerable<Assembly> Assemblies, string prefix)
        {
            var serviceAssembly = Assemblies.Where(x => x.ManifestModule.Name.Contains(prefix)).FirstOrDefault();

            if (serviceAssembly is null) return;

            foreach (var type in serviceAssembly.ExportedTypes)
            {
                var interfaces = type.GetInterfaces().Where(x => x.Namespace.Contains(NAMESPACEBASE));
                if (type.Name.StartsWith("Base") || interfaces.Count() == 0)
                    continue;
                services.AddTransient(interfaces.First(), type);
            }
        }
    }
}