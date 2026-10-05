using ClaudeTestApp.Application.Abstractions;

namespace ClaudeTestApp.AI.Services
{
    public class ChatServiceFactory : IChatServiceFactory
    {
        private readonly IRemoteToolService _remoteToolService;

        public ChatServiceFactory(IRemoteToolService remoteToolService)
        {
            _remoteToolService = remoteToolService;
        }

        public IChatService Create(string systemPrompt, string sessionId)
        {
            return new ChatService(_remoteToolService, systemPrompt, sessionId);
        }

        public IChatService Create()
        {
            return new ChatService(_remoteToolService);
        }
    }
}
