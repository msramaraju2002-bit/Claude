using ClaudeTestApp.AI.Managers;
using ClaudeTestApp.AI.Models;
using System.Text.Json;

namespace ClaudeTestApp.MCPServer.Services
{
    public class OrderService : IOrderService
    {
        public Task<Order?> GetOrderAsync(string orderId)
        {
            Order? order;
            if (!string.IsNullOrEmpty(orderId) && orderId == "1234")
            {
                order= new Order
                {
                    OrderId = orderId,
                    Status = OrderStatus.Shipping,
                };
            }
            order = new Order
            {
                OrderId = orderId,
                Status = OrderStatus.Delivered,
            };

            return Task.FromResult(order);
        }

        public Task<OrderRefund?> GetRefundEligibilityAsync(string orderId)
        {
            OrderRefund? orderRefund;
            orderRefund = new OrderRefund
            {
                OrderId = orderId,
                Status = OrderStatus.Shipping,
                CustomerId = "Customer123",
                RefundEligibility = OrderRefundEligibility.NotEligible

            };
            orderRefund =new OrderRefund
            {
                OrderId = orderId,
                Status = OrderStatus.Shipping,
                CustomerId = "Customer123",
                RefundEligibility = OrderRefundEligibility.Eligible

            };

            return Task.FromResult(orderRefund);
        }
    }
}
