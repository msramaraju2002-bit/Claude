namespace ClaudeTestApp
{
    partial class FrmIntent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelComposer = new Panel();
            statusStrip = new StatusStrip();
            toolStripModel = new ToolStripStatusLabel();
            toolStripInput = new ToolStripStatusLabel();
            toolStripOutput = new ToolStripStatusLabel();
            toolStripStop = new ToolStripStatusLabel();
            toolStripTimeTaken = new ToolStripStatusLabel();
            txtPrompt = new TextBox();
            btnCallAnthropic = new Button();
            panelHeader = new Panel();
            btnClear = new Button();
            rtbChat = new RichTextBox();
            panelComposer.SuspendLayout();
            statusStrip.SuspendLayout();
            panelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // panelComposer
            // 
            panelComposer.Controls.Add(statusStrip);
            panelComposer.Controls.Add(txtPrompt);
            panelComposer.Controls.Add(btnCallAnthropic);
            panelComposer.Dock = DockStyle.Bottom;
            panelComposer.Location = new Point(0, 429);
            panelComposer.Name = "panelComposer";
            panelComposer.Size = new Size(944, 118);
            panelComposer.TabIndex = 14;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripModel, toolStripInput, toolStripOutput, toolStripStop, toolStripTimeTaken });
            statusStrip.Location = new Point(0, 88);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(944, 30);
            statusStrip.TabIndex = 10;
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
            // txtPrompt
            // 
            txtPrompt.Location = new Point(12, 30);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.PlaceholderText = "Ask the agent";
            txtPrompt.Size = new Size(604, 35);
            txtPrompt.TabIndex = 3;
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
            // panelHeader
            // 
            panelHeader.Controls.Add(btnClear);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(944, 44);
            panelHeader.TabIndex = 13;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(1495, 9);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(182, 29);
            btnClear.TabIndex = 6;
            btnClear.Text = "New chat";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // rtbChat
            // 
            rtbChat.Dock = DockStyle.Fill;
            rtbChat.Location = new Point(0, 44);
            rtbChat.Name = "rtbChat";
            rtbChat.Size = new Size(944, 385);
            rtbChat.TabIndex = 15;
            rtbChat.Text = "";
            // 
            // FrmIntent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 547);
            Controls.Add(rtbChat);
            Controls.Add(panelComposer);
            Controls.Add(panelHeader);
            Name = "FrmIntent";
            Text = "Intent";
            panelComposer.ResumeLayout(false);
            panelComposer.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            panelHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelComposer;
        private TextBox txtPrompt;
        private Button btnCallAnthropic;
        private Panel panelHeader;
        private Button btnClear;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripModel;
        private ToolStripStatusLabel toolStripInput;
        private ToolStripStatusLabel toolStripOutput;
        private ToolStripStatusLabel toolStripStop;
        private ToolStripStatusLabel toolStripTimeTaken;
        private RichTextBox rtbChat;
    }
}