public interface IResearchLlm
{
    Task<ResearchFinding> ResearchAsync(
        string systemPrompt,
        string userPrompt,
        CancellationToken cancellationToken);

    Task<string> SynthesizeAsync(
        string prompt,
        CancellationToken cancellationToken);
}
