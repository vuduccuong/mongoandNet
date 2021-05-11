using Microsoft.Extensions.DependencyInjection;

namespace App.Service
{
    public static class DIServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }
    }
}