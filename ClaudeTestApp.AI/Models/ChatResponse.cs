using Anthropic.Models.Messages;
using ClaudeTestApp.AI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ClaudeTestApp.AI.Models
{
    public class ChatResponse
    {
        public string Message { get; set; }

        public string ChatHistory { get; set; }

        public string StopReasonMessage { get; set; }

        public StopReason StopReason { get; set; }

        public long InputTokens { get; set; }

        public long OutputTokens { get; set; }

        public string Model { get; set; }

        public long TimeTaken { get; set; }


        public List<ToolRequest> ToolRequests { get; set; } = new();

        public bool HasToolRequests => ToolRequests.Count > 0;

        public bool ExecutedTool { get; set; } = false;


        public ChatResponse(Anthropic.Models.Messages.Message message, ChatResponseMessageType messageType)
        {

            var anthropicMessage = new StringBuilder();
            ToolService toolService=new ToolService();
            var executeTool = false;

            if (messageType is ChatResponseMessageType.JSON )
            {
                var block = message.Content[0];
                if (block.TryPickText(out var textBlock))
                {
                    anthropicMessage.Append(textBlock.Text);
                }
                else if (block.TryPickToolUse(out var toolUse))
                {
                    anthropicMessage.Append(toolService.ExecuteTool(toolUse,ToolConstants.UseMCP));
                    executeTool = true;
                }

            }
            else
            {
                foreach (var block in message.Content)
                {
                    if (block.TryPickText(out var textBlock))
                    {
                        anthropicMessage.Append(textBlock.Text);
                    }
                    else if (block.TryPickToolUse(out var toolUse))
                    {
                        anthropicMessage.AppendLine(string.Empty);
                        anthropicMessage.Append(toolService.ExecuteTool(toolUse,ToolConstants.UseMCP));
                        executeTool = true;
                    }
                }
            }

            Message = anthropicMessage.ToString();
            StopReason = message.StopReason;
            StopReasonMessage = getStopReasonMessage(StopReason);
            InputTokens = message.Usage.InputTokens;
            OutputTokens = message.Usage.OutputTokens;
            Model = message.Model;
            ExecutedTool = executeTool;
        }

        public string getStopReasonMessage(StopReason? stopReason)
        {
            return stopReason switch
            {
                StopReason.EndTurn =>
                    "Claude completed the response normally.",
                StopReason.MaxTokens =>
                    "Maximum tokens reached.",
                StopReason.StopSequence =>
                    "Claude reached a stop sequence.",
                StopReason.ToolUse =>
                    "Claude requested a tool.",
                StopReason.PauseTurn =>
                    "Claude paused the turn.",
                StopReason.Refusal =>
                    "Claude refused the request.",
                StopReason.ModelContextWindowExceeded =>
                    "Claude reached the context window limit.",
                _ =>
                    $"Unknown stop reason: {stopReason}"
            };
        }
    }
}
