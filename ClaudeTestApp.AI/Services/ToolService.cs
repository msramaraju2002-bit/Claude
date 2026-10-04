using Anthropic.Models.Messages;
using ClaudeTestApp.AI.Managers;
using ClaudeTestApp.AI.Models;
using ModelContextProtocol.Client;
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
        public ToolService() { }

        public string ExecuteTool(
    ToolUseBlock toolUse,bool useMCP)
        {
            if (useMCP)
            {
                // Use MCPService to execute the tool remotely
                McpService mcpService = new McpService();
                var result = mcpService.CallTool(toolUse);
                // Serialize the result to string for consistency
                return JsonSerializer.Serialize(result, JSONSettings.options);
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

        public IReadOnlyList<ToolUnion> GetToolsList(bool mcpServer)
        {
            var toolsList = new List<ToolUnion>();
            McpService mcpService = new McpService();
            if (!mcpServer)
            {
                toolsList = new List<ToolUnion>() { ToolConstants.GetOrderStatusTool, ToolConstants.GetRefundEligiblityTool };

            }
            else
            {
                return mcpService.GetTools();
            }
            return toolsList;
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
