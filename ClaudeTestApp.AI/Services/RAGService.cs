using Anthropic.Helpers.Beta;
using Anthropic.Models.Beta.Messages;
using Anthropic;
using ClaudeTestApp.AI.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Configuration;

namespace ClaudeTestApp.AI.Services
{
    public class RAGService : IRAGService
    {
        private AnthropicClient _client;

        public RAGService()
        {
            string apiKey = ConfigurationManager.AppSettings["AnthropicApiKey"];

            _client = new AnthropicClient
            {
                ApiKey = apiKey
            };
        }

        public async Task<string> AskAsync(string question)
        {
            var searchTool = new BetaRunnableTool
            {
                Name = "search_knowledge_base",

                Definition = new BetaTool
                {
                    Name = "search_knowledge_base",
                    Description =
                        "Search the company knowledge base and return the most relevant documents.",

                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, JsonElement>
                        {
                            ["query"] = JsonSerializer.SerializeToElement(
                                new
                                {
                                    type = "string",
                                    description = "Search query"
                                })
                        },

                        Required = ["query"]
                    }
                },

                Run = async (toolUse,_) =>
                {
                    var query =
                        toolUse.Input.TryGetValue("query", out var q)
                            ? q.GetString() ?? ""
                            : "";

                    // Replace this with your real vector/database search.
                    var results = await SearchKnowledgeBaseAsync(query);

                    return results;
                }
            };

            var runner = _client.Beta.Messages.ToolRunner(
                new MessageCreateParams
                {
                    Model = Anthropic.Models.Messages.Model.ClaudeSonnet5,
                    MaxTokens = 2048,

                    System =
                        """
                    You are a company knowledge assistant.

                    Use the search_knowledge_base tool whenever the
                    answer depends on company documentation.

                    Answer only from the retrieved information.
                    If the information is not available, say so.

                    Include the relevant source names when possible.
                    """,

                    Messages =
                    [
                        new()
                    {
                        Role = Role.User,
                        Content = question
                    }
                    ]
                },
                [searchTool]);

            string finalAnswer = "";

            await foreach (var message in runner)
            {
                foreach (var block in message.Content)
                {
                    if (block.TryPickText(out var textBlock))
                    {
                        finalAnswer += textBlock.Text;
                    }
                }
            }

            return finalAnswer;
        }

        private Task<string> SearchKnowledgeBaseAsync(string query)
        {
            // Example only.
            // Normally:
            // 1. Create embedding for query
            // 2. Vector search
            // 3. Return top K chunks

            var results = new[]
            {
            "Document: Employee Benefits Guide\n" +
            "Employees become eligible for benefits after 30 days.",

            "Document: HR Policy\n" +
            "Benefits enrollment must be completed within 30 days of eligibility."
        };

            return Task.FromResult(string.Join("\n\n", results));
        }
    }
}
