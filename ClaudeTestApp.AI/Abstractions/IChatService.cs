using ClaudeTestApp.AI.Models;
using Microsoft.Extensions.AI;

namespace ClaudeTestApp.AI
{
    public interface IChatService
    {
        Task<Models.ChatResponse> SendMessageAsync(
            List<ChatMessage> chatMessages,
            CancellationToken cancellationToken = default);

        Task<Models.ChatResponse> SendIntentMessageAsync(
            List<ChatMessage> chatMessages,
            CancellationToken cancellationToken = default);
    }
}
