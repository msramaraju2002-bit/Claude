using System.Text.Json;

namespace ClaudeTestApp.Application.Dtos
{
    public record ToolDefinition(
        string Name,
        string Description,
        IReadOnlyDictionary<string, JsonElement> Properties,
        IReadOnlyList<string> Required);
}
