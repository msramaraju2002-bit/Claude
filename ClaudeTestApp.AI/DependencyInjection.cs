using ClaudeTestApp.AI.Services;
using ClaudeTestApp.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ClaudeTestApp.AI
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the Claude chat and RAG services. Requires an <see cref="IRemoteToolService"/> registration.
        /// </summary>
        public static IServiceCollection AddClaudeAI(this IServiceCollection services)
        {
            services.AddSingleton<IChatServiceFactory, ChatServiceFactory>();
            services.AddTransient<IRAGService, RAGService>();
            return services;
        }
    }
}
