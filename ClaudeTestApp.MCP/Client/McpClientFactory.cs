using ModelContextProtocol.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaudeTestApp.MCPServer.Client
{
    public static class McpClientFactory
    {
        public static async Task<McpClient> CreateAsync(string serverAddress, CancellationToken cancellationToken = default)
        {
            var transportOptions = new HttpClientTransportOptions
            {
                Endpoint = new Uri(serverAddress)
            };

            var transport = new HttpClientTransport(transportOptions);

            return await McpClient
                .CreateAsync(transport, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
