using ClaudeTestApp.Domain.Models;

namespace ClaudeTestApp.Application.Abstractions
{
    public interface IOrderService
    {
        Task<Order?> GetOrderAsync(string orderId);

        Task<OrderRefund?> GetRefundEligibilityAsync(string orderId);
    }
}
