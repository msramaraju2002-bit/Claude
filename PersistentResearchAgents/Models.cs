public enum AgentStatus
{
    Pending,
    Running,
    Completed,
    Failed
}

public sealed class ResearchFinding
{
    public string FindingId { get; init; } = Guid.NewGuid().ToString();
    public string Text { get; init; } = "";
    public List<Citation> Citations { get; init; } = [];
}

public sealed class Citation
{
    public string Title { get; init; } = "";
    public string Url { get; init; } = "";
    public string Quote { get; init; } = "";
}

public sealed class AgentCheckpoint
{
    public string ResearchId { get; init; } = "";
    public string AgentId { get; init; } = "";
    public string Strategy { get; init; } = "";
    public AgentStatus Status { get; set; }
    public List<ResearchFinding> Findings { get; set; } = [];
    public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;
    public string? Error { get; set; }
}
