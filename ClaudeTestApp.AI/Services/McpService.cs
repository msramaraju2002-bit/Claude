using Anthropic.Models.Messages;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tool = Anthropic.Models.Messages.Tool;

namespace ClaudeTestApp.AI.Services
{
    public class McpService
    {
        private McpClient? _mcpClient;

        public McpService()
        {
            string mcpServer = ConfigurationManager.AppSettings["McpServer"];
            _mcpClient = McpClientFactory.Create(mcpServer);
        }

        public List<ToolUnion> GetTools()
        {
            List<ToolUnion> toolsList = new List<ToolUnion>();
            var mcpTools = _mcpClient.ListToolsAsync().GetAwaiter().GetResult();
            toolsList = mcpTools
               .Select(mcpTool => ConvertToToolUnion(mcpTool))
                       .ToList();
            return toolsList;


        }


        public CallToolResult CallTool(ToolUseBlock toolUse)
        {
            var arguments = toolUse.Input.ToDictionary(
                kvp => kvp.Key,
                kvp => JsonSerializer.Deserialize<object>(kvp.Value)
            );

            var result = _mcpClient.CallToolAsync(toolUse.Name, arguments).GetAwaiter().GetResult();
            return result;
        }



        private ToolUnion ConvertToToolUnion(McpClientTool mcpTool)
        {
            var tool = new Tool
            {
                Name = mcpTool.Name,
                Description = mcpTool.Description ?? string.Empty,

                InputSchema = ConvertInputSchema(
                    mcpTool.JsonSchema)
            };

            return new ToolUnion(tool);
        }

        private InputSchema ConvertInputSchema(JsonElement jsonSchema)
        {
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

            return new InputSchema
            {
                Properties = properties,
                Required = required
            };
        }
    }
}
