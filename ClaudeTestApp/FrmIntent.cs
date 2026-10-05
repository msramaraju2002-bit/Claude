using ClaudeTestApp.AI.Services;
using ClaudeTestApp.AI;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClaudeTestApp.AI.Extensions;

namespace ClaudeTestApp
{
    public partial class FrmIntent : Form
    {
        List<ChatMessage> chatMessages = new List<ChatMessage>();
        private readonly IChatServiceFactory _chatServiceFactory;

        string prompt = """
                            Classify the customer's message into one of these intents:
                            - refund_request
                            - order_status
                            - billing_issue
                            - product_question
                            - other
                            Customer message:
                            {customer_message}

                            Return only a valid JSON object.
                            Do not include markdown.
                            Do not include explanations.
                            Do not wrap the JSON in a code block.
                            {{
                              "intent": "refund_request"
                            }}
                       """;

        public FrmIntent(IChatServiceFactory chatServiceFactory)
        {
            InitializeComponent();
            _chatServiceFactory = chatServiceFactory;
        }

        private async void btnCallAnthropic_Click(object sender, EventArgs e)
        {
            IChatService chatManager = _chatServiceFactory.Create();
            chatMessages.Clear();
            string customerMessage = prompt.Replace("{customer_message}", txtPrompt.Text);
            chatMessages.AddUserMessage(customerMessage);
            AddChatMessage("You", txtPrompt.Text, true);

            var taskResponse = await chatManager.SendIntentMessageAsync(chatMessages);
            var chatResponse = taskResponse.Message.ToString();

            AddChatMessage("Chat assistant", chatResponse, false);
            chatMessages.AddAssistantMessage(chatResponse);
            txtPrompt.Text = string.Empty;
            UpdateStatus(taskResponse);
        }

        private void AddChatMessage(
    string sender,
    string message,
    bool isUser)
        {
            // Sender
            rtbChat.SelectionFont =
                new Font(rtbChat.Font, FontStyle.Bold);

            rtbChat.SelectionColor =
       isUser ? Color.Green : Color.Blue;


            rtbChat.AppendText(
                $"{sender}:{Environment.NewLine}");

            // Message
            rtbChat.SelectionFont =
                new Font(rtbChat.Font, FontStyle.Regular);

            rtbChat.AppendText(message);

            rtbChat.AppendText(
                Environment.NewLine +
                Environment.NewLine);

            // Scroll to latest message
            rtbChat.SelectionStart = rtbChat.TextLength;
            rtbChat.ScrollToCaret();
        }

        private void UpdateStatus(AI.Models.ChatResponse response)
        {
            toolStripModel.Text = $"Model: {response.Model}";
            toolStripInput.Text = $"Input Tokens: {response.InputTokens}";
            toolStripOutput.Text = $"Output Tokens: {response.OutputTokens}";
            toolStripTimeTaken.Text =
                $"Time: {response.TimeTaken:F2} sec";
        }
    }
}
