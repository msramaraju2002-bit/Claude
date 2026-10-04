using ClaudeTestApp.AI.Models;

namespace ClaudeTestApp.MCPServer.Services
{
    public interface IOrderService
    {
        Task<Order?> GetOrderAsync(string orderId);

        Task<OrderRefund?> GetRefundEligibilityAsync(string orderId);
    }
}
