using ClaudeTestApp.Application.Abstractions;
using ClaudeTestApp.Application.Common;
using ClaudeTestApp.Application.Dtos;
using ModelContextProtocol.Client;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using System.Text.Json;

namespace ClaudeTestApp.MCPServer.Client
{
    public class McpToolClient : IRemoteToolService
    {
        public async Task<IReadOnlyList<ToolDefinition>> GetToolsAsync(CancellationToken cancellationToken = default)
        {
            var mcpClient = await CreateClientAsync(cancellationToken).ConfigureAwait(false);
            var mcpTools = await mcpClient.ListToolsAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            return mcpTools
               .Select(ConvertToToolDefinition)
               .ToList();
        }

        public async Task<string> CallToolAsync(
            string toolName,
            IReadOnlyDictionary<string, JsonElement> input,
            CancellationToken cancellationToken = default)
        {
            var mcpClient = await CreateClientAsync(cancellationToken).ConfigureAwait(false);

            var arguments = input.ToDictionary(
                kvp => kvp.Key,
                kvp => JsonSerializer.Deserialize<object>(kvp.Value)
            );

            var result = await mcpClient.CallToolAsync(toolName, arguments, cancellationToken: cancellationToken).ConfigureAwait(false);
            // Serialize the result to string for consistency
            return JsonSerializer.Serialize(result, JSONSettings.options);
        }

        private static Task<McpClient> CreateClientAsync(CancellationToken cancellationToken)
        {
            string mcpServer = ConfigurationManager.AppSettings["McpServer"];
            return McpClientFactory.CreateAsync(mcpServer, cancellationToken);
        }

        private static ToolDefinition ConvertToToolDefinition(McpClientTool mcpTool)
        {
            var jsonSchema = mcpTool.JsonSchema;
            var properties = new Dictionary<string, JsonElement>();

            if (jsonSchema.TryGetProperty("properties", out var propertiesElement))
            {
                foreach (var property in propertiesElement.EnumerateObject())
                {
                    properties[property.Name] = property.Value.Clone();
                }
            }

            var required = new List<string>();

            if (jsonSchema.TryGetProperty("required", out var requiredElement) &&
                requiredElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in requiredElement.EnumerateArray())
                {
                    var value = item.GetString();

                    if (!string.IsNullOrEmpty(value))
                        required.Add(value);
                }
            }

            return new ToolDefinition(
                mcpTool.Name,
                mcpTool.Description ?? string.Empty,
                properties,
                required);
        }
    }
}
