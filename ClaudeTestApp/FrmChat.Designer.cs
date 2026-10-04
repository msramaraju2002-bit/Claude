namespace ClaudeTestApp
{
    partial class FrmChat
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCallAnthropic = new Button();
            txtPrompt = new TextBox();
            btnClear = new Button();
            statusStrip = new StatusStrip();
            toolStripModel = new ToolStripStatusLabel();
            toolStripInput = new ToolStripStatusLabel();
            toolStripOutput = new ToolStripStatusLabel();
            toolStripStop = new ToolStripStatusLabel();
            toolStripTimeTaken = new ToolStripStatusLabel();
            panelHeader = new Panel();
            pnlConversation = new Panel();
            rtbChat = new RichTextBox();
            button1 = new Button();
            panelComposer = new Panel();
            statusStrip.SuspendLayout();
            panelHeader.SuspendLayout();
            pnlConversation.SuspendLayout();
            panelComposer.SuspendLayout();
            SuspendLayout();
            // 
            // btnCallAnthropic
            // 
            btnCallAnthropic.Location = new Point(650, 36);
            btnCallAnthropic.Name = "btnCallAnthropic";
            btnCallAnthropic.Size = new Size(182, 29);
            btnCallAnthropic.TabIndex = 0;
            btnCallAnthropic.Text = "Send";
            btnCallAnthropic.UseVisualStyleBackColor = true;
            btnCallAnthropic.Click += btnCallAnthropic_Click;
            // 
            // txtPrompt
            // 
            txtPrompt.Location = new Point(12, 30);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.PlaceholderText = "Ask the agent";
            txtPrompt.Size = new Size(604, 35);
            txtPrompt.TabIndex = 3;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(1495, 9);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(182, 29);
            btnClear.TabIndex = 6;
            btnClear.Text = "New chat";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += button1_Click;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripModel, toolStripInput, toolStripOutput, toolStripStop, toolStripTimeTaken });
            statusStrip.Location = new Point(0, 832);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1700, 30);
            statusStrip.TabIndex = 9;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripModel
            // 
            toolStripModel.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripModel.Name = "toolStripModel";
            toolStripModel.Size = new Size(67, 24);
            toolStripModel.Text = "Model  :";
            // 
            // toolStripInput
            // 
            toolStripInput.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripInput.Name = "toolStripInput";
            toolStripInput.Size = new Size(58, 24);
            toolStripInput.Text = "Input : ";
            // 
            // toolStripOutput
            // 
            toolStripOutput.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripOutput.Name = "toolStripOutput";
            toolStripOutput.Size = new Size(66, 24);
            toolStripOutput.Text = "Output :";
            // 
            // toolStripStop
            // 
            toolStripStop.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStop.Name = "toolStripStop";
            toolStripStop.Size = new Size(107, 24);
            toolStripStop.Text = "Stop Reason : ";
            // 
            // toolStripTimeTaken
            // 
            toolStripTimeTaken.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripTimeTaken.Name = "toolStripTimeTaken";
            toolStripTimeTaken.Size = new Size(97, 24);
            toolStripTimeTaken.Text = "Time taken : ";
            // 
            // panelHeader
            // 
            panelHeader.BorderStyle = BorderStyle.FixedSingle;
            panelHeader.Controls.Add(btnClear);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1700, 44);
            panelHeader.TabIndex = 10;
            // 
            // pnlConversation
            // 
            pnlConversation.BorderStyle = BorderStyle.FixedSingle;
            pnlConversation.Controls.Add(rtbChat);
            pnlConversation.Controls.Add(button1);
            pnlConversation.Dock = DockStyle.Fill;
            pnlConversation.Location = new Point(0, 44);
            pnlConversation.Name = "pnlConversation";
            pnlConversation.Size = new Size(1700, 818);
            pnlConversation.TabIndex = 11;
            // 
            // rtbChat
            // 
            rtbChat.Dock = DockStyle.Fill;
            rtbChat.Location = new Point(0, 0);
            rtbChat.Margin = new Padding(0);
            rtbChat.Name = "rtbChat";
            rtbChat.Size = new Size(1698, 816);
            rtbChat.TabIndex = 7;
            rtbChat.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(1495, 9);
            button1.Name = "button1";
            button1.Size = new Size(182, 29);
            button1.TabIndex = 6;
            button1.Text = "New chat";
            button1.UseVisualStyleBackColor = true;
            // 
            // panelComposer
            // 
            panelComposer.Controls.Add(txtPrompt);
            panelComposer.Controls.Add(btnCallAnthropic);
            panelComposer.Dock = DockStyle.Bottom;
            panelComposer.Location = new Point(0, 714);
            panelComposer.Name = "panelComposer";
            panelComposer.Size = new Size(1700, 118);
            panelComposer.TabIndex = 12;
            // 
            // FrmChat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1700, 862);
            Controls.Add(panelComposer);
            Controls.Add(statusStrip);
            Controls.Add(pnlConversation);
            Controls.Add(panelHeader);
            Name = "FrmChat";
            Text = "Shop assist agent";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            panelHeader.ResumeLayout(false);
            pnlConversation.ResumeLayout(false);
            panelComposer.ResumeLayout(false);
            panelComposer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCallAnthropic;
        private TextBox txtPrompt;
        private Button btnClear;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripModel;
        private ToolStripStatusLabel toolStripInput;
        private ToolStripStatusLabel toolStripOutput;
        private ToolStripStatusLabel toolStripStop;
        private ToolStripStatusLabel toolStripTimeTaken;
        private Panel panelHeader;
        private Panel pnlConversation;
        private Button button1;
        private Panel panelComposer;
        private RichTextBox rtbChat;
    }
}
