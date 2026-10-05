using Anthropic.Models.Messages;
using ClaudeTestApp.AI.Models;
using ClaudeTestApp.Application.Abstractions;
using ClaudeTestApp.Application.Common;
using ClaudeTestApp.Application.Dtos;
using ClaudeTestApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClaudeTestApp.AI.Services
{
    internal class ToolService
    {
        private readonly IRemoteToolService _remoteToolService;

        public ToolService(IRemoteToolService remoteToolService)
        {
            _remoteToolService = remoteToolService;
        }

        public string ExecuteTool(
    ToolUseBlock toolUse,bool useMCP)
        {
            if (useMCP)
            {
                // Execute the tool remotely on the MCP server
                return _remoteToolService
                    .CallToolAsync(toolUse.Name, toolUse.Input)
                    .GetAwaiter()
                    .GetResult();
            }

            var orderId =
                      toolUse.Input["orderId"].GetString();
            switch (toolUse.Name)
            {
                case "GetOrderStatus":
                    return GetOrderStatus(orderId!);

                case "GetRefundEligibility":
                    return GetRefundEligibility(orderId!);

                default:
                    throw new InvalidOperationException(
                        $"Unknown tool: {toolUse.Name}");
            }
        }

        private string GetRefundEligibility(string orderId)
        {
            if (!string.IsNullOrEmpty(orderId) && orderId == "1234")
            {
                return JsonSerializer.Serialize(new OrderRefund
                {
                    OrderId = orderId,
                    Status = OrderStatus.Shipping,
                    CustomerId = "Customer123",
                    RefundEligibility = OrderRefundEligibility.NotEligible

                }, JSONSettings.options);
            }
            return JsonSerializer.Serialize(new OrderRefund
            {
                OrderId = orderId,
                Status = OrderStatus.Shipping,
                CustomerId = "Customer123",
                RefundEligibility = OrderRefundEligibility.Eligible

            }, JSONSettings.options);
        }

        public async Task<IReadOnlyList<ToolUnion>> GetToolsListAsync(bool mcpServer, CancellationToken cancellationToken = default)
        {
            var toolsList = new List<ToolUnion>();
            if (!mcpServer)
            {
                toolsList = new List<ToolUnion>() { ToolConstants.GetOrderStatusTool, ToolConstants.GetRefundEligiblityTool };

            }
            else
            {
                var remoteTools = await _remoteToolService.GetToolsAsync(cancellationToken);
                return remoteTools.Select(ConvertToToolUnion).ToList();
            }
            return toolsList;
        }

        private static ToolUnion ConvertToToolUnion(ToolDefinition toolDefinition)
        {
            var tool = new Tool
            {
                Name = toolDefinition.Name,
                Description = toolDefinition.Description,

                InputSchema = new InputSchema
                {
                    Properties = new Dictionary<string, JsonElement>(toolDefinition.Properties),
                    Required = toolDefinition.Required.ToList()
                }
            };

            return new ToolUnion(tool);
        }

  
        private string GetOrderStatus(string orderId)
        {
            if (!string.IsNullOrEmpty(orderId) && orderId == "1234")
            {
                return JsonSerializer.Serialize(new Order
                {
                    OrderId = orderId,
                    Status = OrderStatus.Shipping,
                }, JSONSettings.options);
            }
            return JsonSerializer.Serialize(new Order
            {
                OrderId = orderId,
                Status = OrderStatus.Delivered,
            }, JSONSettings.options);
        }
    }
}
