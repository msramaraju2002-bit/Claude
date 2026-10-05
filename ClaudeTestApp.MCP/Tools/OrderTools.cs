using ClaudeTestApp.Domain.Models;
using ClaudeTestApp.Application.Abstractions;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace ClaudeTestApp.MCPServer.Tools
{
    [McpServerToolType]
    public class OrderTools
    {
        private readonly IOrderService _orderService;

        public OrderTools(IOrderService orderService) {
            _orderService= orderService;
        }


        [McpServerTool]
        [Description("Gets the current status of an order.")]
        private async Task<Order?> GetOrderStatus(string orderId)
        {
            return await _orderService.GetOrderAsync(orderId);
        }
    }
}
