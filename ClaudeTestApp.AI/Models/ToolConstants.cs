using Anthropic.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClaudeTestApp.AI.Models
{
    internal static class ToolConstants
    {
        public static bool UseMCP = true;
        public static readonly Tool GetOrderStatusTool = new Tool
        {
            Name = "GetOrderStatus",

            Description =
                    "Gets the current status of an order. " +
                    "Use this tool when the customer asks about an order status.",

            InputSchema = new InputSchema
            {
                Properties = new Dictionary<string, JsonElement>
                {
                    ["orderId"] = JsonSerializer.SerializeToElement(
                        new
                        {
                            type = "string",
                            description = "The order ID provided by the customer"
                        })
                },

                Required = ["orderId"]
            }
        };

        public static readonly Tool GetRefundEligiblityTool = new Tool
        {
            Name = "GetRefundEligibility",

            Description =
                    "Check refund eligibility of the order. " +
                    "Use this tool to check refund eligibility.",

            InputSchema = new InputSchema
            {
                Properties = new Dictionary<string, JsonElement>
                {
                    ["orderId"] = JsonSerializer.SerializeToElement(
                        new
                        {
                            type = "string",
                            description = "The order ID provided by the customer"
                        })
                },

                Required = ["orderId"]
            }
        };
    }
}
