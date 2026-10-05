using ClaudeTestApp.Application.Abstractions;
using ClaudeTestApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClaudeTestApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
