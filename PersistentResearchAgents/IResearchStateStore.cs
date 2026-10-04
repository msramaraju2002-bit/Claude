public interface IResearchStateStore
{
    Task SaveAsync(AgentCheckpoint checkpoint, CancellationToken cancellationToken = default);

    Task<AgentCheckpoint?> GetAsync(
        string researchId,
        string agentId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AgentCheckpoint>> GetAllAsync(
        string researchId,
        CancellationToken cancellationToken = default);
}
