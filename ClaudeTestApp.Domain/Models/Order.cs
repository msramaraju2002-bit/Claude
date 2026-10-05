using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaudeTestApp.Domain.Models
{
    public class OrderDetails
    {
        public string OrderId { get; set; } = "";
        public string CustomerId { get; set; } = "";
        public OrderStatus Status { get; set; } = OrderStatus.Created;
    }

    public class Order : OrderDetails
    {

    }

    public class OrderRefund : OrderDetails
    {
        public OrderRefundEligibility RefundEligibility { get; set; } = OrderRefundEligibility.NotEligible;
    }

    public enum OrderStatus
    {
        Created,
        Delivered,
        Shipping,
        CreatedReturned,
        Returned
    }

    public enum OrderRefundEligibility
    {
        Eligible,
        NotEligible
    }
}
