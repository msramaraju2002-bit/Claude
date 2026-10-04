using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaudeTestApp.AI.Extensions
{
    public static class ChatMessageExtensions
    {
        public static void AddUserMessage(
            this List<ChatMessage> messages,
            string message)
        {
            messages.Add(
                new ChatMessage(
                    ChatRole.User,
                    message));
        }

        public static void AddAssistantMessage(
            this List<ChatMessage> messages,
            string message)
        {
            messages.Add(
                new ChatMessage(
                    ChatRole.Assistant,
                    message));
        }

        public static void AddSystemMessage(
            this List<ChatMessage> messages,
            string message)
        {
            messages.Add(
                new ChatMessage(
                    ChatRole.System,
                    message));
        }


        public static List<ChatMessage> Reset(
        this List<ChatMessage> messages)
        {
            messages.Clear();
            return messages;
        }
    }
}
