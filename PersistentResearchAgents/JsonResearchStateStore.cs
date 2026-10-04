using System.Text.Json;

public sealed class JsonResearchStateStore : IResearchStateStore
{
    private readonly string _directory;
    private readonly SemaphoreSlim _lock = new(1, 1);

    public JsonResearchStateStore(string directory)
    {
        _directory = directory;
        Directory.CreateDirectory(directory);
    }

    public async Task SaveAsync(
        AgentCheckpoint checkpoint,
        CancellationToken cancellationToken = default)
    {
        checkpoint.UpdatedUtc = DateTime.UtcNow;
        string path = GetPath(checkpoint.ResearchId, checkpoint.AgentId);

        string json = JsonSerializer.Serialize(
            checkpoint,
            new JsonSerializerOptions { WriteIndented = true });

        await _lock.WaitAsync(cancellationToken);
        try
        {
            string temp = path + ".tmp";
            await File.WriteAllTextAsync(temp, json, cancellationToken);
            File.Move(temp, path, overwrite: true);
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task<AgentCheckpoint?> GetAsync(
        string researchId,
        string agentId,
        CancellationToken cancellationToken = default)
    {
        string path = GetPath(researchId, agentId);
        if (!File.Exists(path))
            return null;

        string json = await File.ReadAllTextAsync(path, cancellationToken);
        return JsonSerializer.Deserialize<AgentCheckpoint>(json);
    }

    public async Task<IReadOnlyList<AgentCheckpoint>> GetAllAsync(
        string researchId,
        CancellationToken cancellationToken = default)
    {
        var results = new List<AgentCheckpoint>();

        foreach (string file in Directory.GetFiles(_directory, $"{researchId}_*.json"))
        {
            string json = await File.ReadAllTextAsync(file, cancellationToken);
            var checkpoint = JsonSerializer.Deserialize<AgentCheckpoint>(json);

            if (checkpoint != null)
                results.Add(checkpoint);
        }

        return results;
    }

    private string GetPath(string researchId, string agentId) =>
        Path.Combine(_directory, $"{researchId}_{agentId}.json");
}
