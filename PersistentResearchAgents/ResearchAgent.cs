public sealed class ResearchAgent
{
    private readonly IResearchLlm _llm;
    private readonly IResearchStateStore _store;

    public ResearchAgent(IResearchLlm llm, IResearchStateStore store)
    {
        _llm = llm;
        _store = store;
    }

    public async Task RunAsync(
        string researchId,
        string agentId,
        string strategy,
        string topic,
        CancellationToken cancellationToken = default)
    {
        var checkpoint =
            await _store.GetAsync(researchId, agentId, cancellationToken)
            ?? new AgentCheckpoint
            {
                ResearchId = researchId,
                AgentId = agentId,
                Strategy = strategy,
                Status = AgentStatus.Pending
            };

        if (checkpoint.Status == AgentStatus.Completed)
            return;

        try
        {
            checkpoint.Status = AgentStatus.Running;
            await _store.SaveAsync(checkpoint, cancellationToken);

            string systemPrompt = $"""
                You are a pharmaceutical policy research agent.

                Research strategy:
                {strategy}

                Produce evidence-based findings.
                Every factual finding must contain its citations.
                Never return a citation separately from the finding that depends on it.
                """;

            string userPrompt = $"""
                Research:
                {topic}

                Explore the issue specifically from this perspective:
                {strategy}
                """;

            ResearchFinding finding = await _llm.ResearchAsync(
                systemPrompt, userPrompt, cancellationToken);

            checkpoint.Findings.Add(finding);

            // Persist findings/citations immediately.
            await _store.SaveAsync(checkpoint, cancellationToken);

            checkpoint.Status = AgentStatus.Completed;
            await _store.SaveAsync(checkpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            checkpoint.Status = AgentStatus.Failed;
            checkpoint.Error = ex.ToString();

            await _store.SaveAsync(checkpoint, CancellationToken.None);
            throw;
        }
    }
}
