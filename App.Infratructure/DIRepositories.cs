using Microsoft.Extensions.DependencyInjection;

namespace App.Infratructure
{
    public static class DIRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}