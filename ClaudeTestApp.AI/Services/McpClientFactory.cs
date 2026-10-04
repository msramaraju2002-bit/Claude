using ModelContextProtocol.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaudeTestApp.AI.Services
{
    public static class McpClientFactory
    {
        public static McpClient Create(string serverAddress)
        {
            var transportOptions = new HttpClientTransportOptions
            {
                Endpoint = new Uri(serverAddress)
            };

            var transport = new HttpClientTransport(transportOptions);

            return McpClient
                .CreateAsync(transport)
                .GetAwaiter()
                .GetResult();
        }
    }
}
