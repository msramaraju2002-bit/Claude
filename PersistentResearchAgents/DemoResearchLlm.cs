public sealed class DemoResearchLlm : IResearchLlm
{
    public Task<ResearchFinding> ResearchAsync(
        string systemPrompt,
        string userPrompt,
        CancellationToken cancellationToken)
    {
        // Replace this implementation with Anthropic/OpenAI/etc.
        var finding = new ResearchFinding
        {
            Text = "Demo finding. Replace DemoResearchLlm with your actual LLM implementation.",
            Citations =
            [
                new Citation
                {
                    Title = "Demo source",
                    Url = "https://example.com",
                    Quote = "Demo evidence"
                }
            ]
        };

        return Task.FromResult(finding);
    }

    public Task<string> SynthesizeAsync(
        string prompt,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            "Demo synthesis completed. Connect IResearchLlm to your preferred model.");
    }
}
