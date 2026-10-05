using ClaudeTestApp.Application.Dtos;
using System.Text.Json;

namespace ClaudeTestApp.Application.Abstractions
{
    /// <summary>
    /// Discovers and executes tools hosted by a remote tool server (e.g. an MCP server).
    /// </summary>
    public interface IRemoteToolService
    {
        Task<IReadOnlyList<ToolDefinition>> GetToolsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls a remote tool and returns its result serialized as JSON.
        /// </summary>
        Task<string> CallToolAsync(
            string toolName,
            IReadOnlyDictionary<string, JsonElement> input,
            CancellationToken cancellationToken = default);
    }
}
