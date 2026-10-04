using System.Text;

public sealed class ResearchCoordinator
{
    private readonly ResearchAgent _agent;
    private readonly IResearchStateStore _store;
    private readonly IResearchLlm _llm;

    public ResearchCoordinator(
        ResearchAgent agent,
        IResearchStateStore store,
        IResearchLlm llm)
    {
        _agent = agent;
        _store = store;
        _llm = llm;
    }

    public async Task<(string ResearchId, string Report)> ExecuteAsync(
        string topic,
        CancellationToken cancellationToken = default)
    {
        string researchId = Guid.NewGuid().ToString("N");

        await Task.WhenAll(
            _agent.RunAsync(
                researchId,
                "legal-risk",
                "Analyze legal/regulatory risks, compliance obligations and litigation exposure.",
                topic,
                cancellationToken),

            _agent.RunAsync(
                researchId,
                "market-impact",
                "Analyze pricing, competition, manufacturers, payers and commercial incentives.",
                topic,
                cancellationToken),

            _agent.RunAsync(
                researchId,
                "public-health",
                "Analyze patient outcomes, access, safety and healthcare-system consequences.",
                topic,
                cancellationToken));

        string report = await SynthesizeAsync(researchId, topic, cancellationToken);
        return (researchId, report);
    }

    public async Task<string> ResumeAsync(
        string researchId,
        string topic,
        CancellationToken cancellationToken = default)
    {
        var expectedBranches = new[]
        {
            (Id: "legal-risk",
             Strategy: "Analyze legal/regulatory risks, compliance obligations and litigation exposure."),
            (Id: "market-impact",
             Strategy: "Analyze pricing, competition, manufacturers, payers and commercial incentives."),
            (Id: "public-health",
             Strategy: "Analyze patient outcomes, access, safety and healthcare-system consequences.")
        };

        foreach (var branch in expectedBranches)
        {
            var checkpoint =
                await _store.GetAsync(researchId, branch.Id, cancellationToken);

            if (checkpoint?.Status == AgentStatus.Completed)
                continue;

            await _agent.RunAsync(
                researchId,
                branch.Id,
                branch.Strategy,
                topic,
                cancellationToken);
        }

        return await SynthesizeAsync(researchId, topic, cancellationToken);
    }

    private async Task<string> SynthesizeAsync(
        string researchId,
        string topic,
        CancellationToken cancellationToken)
    {
        // Reload from durable state, never from conversation/session memory.
        IReadOnlyList<AgentCheckpoint> branches =
            await _store.GetAllAsync(researchId, cancellationToken);

        var completed = branches
            .Where(x => x.Status == AgentStatus.Completed)
            .ToList();

        if (completed.Count != 3)
            throw new InvalidOperationException(
                $"Expected 3 completed branches, found {completed.Count}.");

        string evidence = BuildEvidence(completed);

        string prompt = $"""
            You are the research coordinator.

            Topic:
            {topic}

            Produce an integrated report covering:
            1. Legal/regulatory risk
            2. Market impact
            3. Public-health impact
            4. Areas where branches agree
            5. Areas where evidence conflicts
            6. Important uncertainties
            7. Sources/citations

            Do not invent citations.
            Preserve citations associated with each finding.

            RESEARCH EVIDENCE
            =================
            {evidence}
            """;

        return await _llm.SynthesizeAsync(prompt, cancellationToken);
    }

    private static string BuildEvidence(IEnumerable<AgentCheckpoint> branches)
    {
        var output = new StringBuilder();

        foreach (var branch in branches)
        {
            output.AppendLine($"## {branch.AgentId}: {branch.Strategy}");

            foreach (var finding in branch.Findings)
            {
                output.AppendLine(finding.Text);

                foreach (var citation in finding.Citations)
                {
                    output.AppendLine($"SOURCE: {citation.Title}");
                    output.AppendLine($"URL: {citation.Url}");

                    if (!string.IsNullOrWhiteSpace(citation.Quote))
                        output.AppendLine($"EVIDENCE: {citation.Quote}");
                }

                output.AppendLine();
            }
        }

        return output.ToString();
    }
}
