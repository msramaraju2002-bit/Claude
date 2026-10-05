namespace ClaudeTestApp.AI
{
    public interface IChatServiceFactory
    {
        IChatService Create(string systemPrompt, string sessionId);

        IChatService Create();
    }
}
