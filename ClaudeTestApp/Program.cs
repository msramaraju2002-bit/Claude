using ClaudeTestApp.AI;
using ClaudeTestApp.MCPServer.Client;
using Microsoft.Extensions.DependencyInjection;

namespace ClaudeTestApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            services.AddMcpToolClient();
            services.AddClaudeAI();
            services.AddTransient<FrmChat>();
            services.AddTransient<FrmIntent>();

            using var serviceProvider = services.BuildServiceProvider();

            // Fully qualified: ClaudeTestApp.Application (project namespace) would otherwise shadow it.
            System.Windows.Forms.Application.Run(serviceProvider.GetRequiredService<FrmChat>());
        }
    }
}
