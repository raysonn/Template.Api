using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Uninter.Template.CrossCutting
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddController(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().AddRequirements().RequireAuthenticatedUser().Build()));
            })
              .AddJsonOptions(o => o.JsonSerializerOptions.WriteIndented = true);

            return services;
        }
    }
}