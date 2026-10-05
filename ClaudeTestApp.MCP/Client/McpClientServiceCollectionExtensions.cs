using ClaudeTestApp.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ClaudeTestApp.MCPServer.Client
{
    public static class McpClientServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the MCP client used to discover and call tools on the MCP server.
        /// </summary>
        public static IServiceCollection AddMcpToolClient(this IServiceCollection services)
        {
            services.AddSingleton<IRemoteToolService, McpToolClient>();
            return services;
        }
    }
}
