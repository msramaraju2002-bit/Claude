using ClaudeTestApp.AI.Models;
using ClaudeTestApp.MCPServer.Services;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace ClaudeTestApp.MCPServer.Tools
{
    [McpServerToolType]
    public class RefundTools
    {
        private readonly IOrderService _orderService;
        public RefundTools(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [McpServerTool]
        [Description("Gets the refund eligibility status.")]
        private async Task<OrderRefund?> GetRefundEligibility(string orderId)
        {
            return await _orderService.GetRefundEligibilityAsync(orderId);
        }
    }
}
