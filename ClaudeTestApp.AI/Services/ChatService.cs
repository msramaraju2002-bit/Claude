using Anthropic;
using Anthropic.Models.Messages;
using ClaudeTestApp.AI.Models;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClaudeTestApp.AI.Extensions;
using ClaudeTestApp.Application.Abstractions;
using Role = Anthropic.Models.Messages.Role;

namespace ClaudeTestApp.AI.Services
{
    public class ChatService : IChatService
    {
        private AnthropicClient _client;
        private string _systemPrompt;
        private readonly ToolService _toolService;

        public ChatService(IRemoteToolService remoteToolService, string systemPrompt,string sessionId) {
            string apiKey = ConfigurationManager.AppSettings["AnthropicApiKey"];

            _client = new AnthropicClient
            {
                ApiKey = apiKey
            };
            _systemPrompt = systemPrompt;
            _toolService = new ToolService(remoteToolService);
        }

        public ChatService(IRemoteToolService remoteToolService)
        {
            string apiKey = ConfigurationManager.AppSettings["AnthropicApiKey"];

            _client = new AnthropicClient
            {
                ApiKey = apiKey
            };
            _toolService = new ToolService(remoteToolService);
        }

        /// <summary>
        /// Sends a message to the Claude API and returns the response.
        /// </summary>
        /// <param name="chatMessages"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Models.ChatResponse> SendMessageAsync(List<ChatMessage> chatMessages, CancellationToken cancellationToken = default)
        {
            ToolService toolService = _toolService;
            var anthropicMessages = chatMessages
            .Where(m => m.Role == ChatRole.User ||
                        m.Role == ChatRole.Assistant)
            .Select(m => new MessageParam
            {
                Role = m.Role == ChatRole.User
                    ? Role.User
                    : Role.Assistant,

                Content = m.Text ?? string.Empty
            })
            .ToList();
            var stopwatch = Stopwatch.StartNew();
            Message message = await GetMessage(toolService, anthropicMessages);

            Models.ChatResponse claudeResponse = new Models.ChatResponse(message, ChatResponseMessageType.Text, toolService);

            while(claudeResponse.ExecutedTool)
            {
                // Add the tool response to the chat messages
                chatMessages.AddUserMessage(claudeResponse.Message);
                // Get the next message from Claude with the updated chat messages
                anthropicMessages = chatMessages
                    .Where(m => m.Role == ChatRole.User ||
                                m.Role == ChatRole.Assistant)
                    .Select(m => new MessageParam
                    {
                        Role = m.Role == ChatRole.User
                            ? Role.User
                            : Role.Assistant,
                        Content = m.Text ?? string.Empty
                    })
                    .ToList();
                message = await GetMessage(toolService, anthropicMessages);
                claudeResponse = new Models.ChatResponse(message, ChatResponseMessageType.Text, toolService);
            }

            stopwatch.Stop();
            claudeResponse.TimeTaken = stopwatch.ElapsedMilliseconds;

            return claudeResponse;
        }

        private async Task<Message> GetMessage(ToolService toolService, List<MessageParam> anthropicMessages)
        {
            return await _client.Messages.Create(
                            new MessageCreateParams
                            {
                                Model = "claude-sonnet-4-5-20250929",
                                MaxTokens = 1024,
                                Messages = anthropicMessages,
                                System = new MessageCreateParamsSystem(
                                    new List<TextBlockParam>
                                    {
                            new TextBlockParam
                            {
                                Text = _systemPrompt,
                                CacheControl = new CacheControlEphemeral()
                            }
                                    }
                                ),
                                Tools = await toolService.GetToolsListAsync(true),
                                ToolChoice = new Anthropic.Models.Messages.ToolChoice(new ToolChoiceAuto())


                            });
        }



        /// <summary>
        /// Sends a message to the Claude API and returns the response.
        /// </summary>
        /// <param name="chatMessages"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Models.ChatResponse> SendIntentMessageAsync(List<ChatMessage> chatMessages, CancellationToken cancellationToken = default)
        {
            var anthropicMessages = chatMessages
            .Where(m => m.Role == ChatRole.User ||
                        m.Role == ChatRole.Assistant)
            .Select(m => new MessageParam
            {
                Role = m.Role == ChatRole.User
                    ? Role.User
                    : Role.Assistant,

                Content = m.Text ?? string.Empty
            })
            .ToList();
            var stopwatch = Stopwatch.StartNew();
            var message = await _client.Messages.Create(
                new MessageCreateParams
                {
                    Model = "claude-sonnet-4-5-20250929",
                    MaxTokens = 1024,
                    Messages = anthropicMessages
                });

            stopwatch.Stop();

            Models.ChatResponse claudeResponse = new Models.ChatResponse(message,ChatResponseMessageType.JSON, _toolService);
            claudeResponse.TimeTaken = stopwatch.ElapsedMilliseconds;

            return claudeResponse;
        }
 
    }
}
