namespace AIBots
{
    partial class Main
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
            this.btnAIBots = new System.Windows.Forms.Button();
            this.btnHarvestBots = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAIBots
            // 
            this.btnAIBots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAIBots.Location = new System.Drawing.Point(12, 12);
            this.btnAIBots.Name = "btnAIBots";
            this.btnAIBots.Size = new System.Drawing.Size(222, 23);
            this.btnAIBots.TabIndex = 0;
            this.btnAIBots.Text = "AI bots";
            this.btnAIBots.UseVisualStyleBackColor = true;
            this.btnAIBots.Click += new System.EventHandler(this.btnAIBots_Click);
            // 
            // btnHarvestBots
            // 
            this.btnHarvestBots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHarvestBots.Location = new System.Drawing.Point(12, 41);
            this.btnHarvestBots.Name = "btnHarvestBots";
            this.btnHarvestBots.Size = new System.Drawing.Size(222, 23);
            this.btnHarvestBots.TabIndex = 1;
            this.btnHarvestBots.Text = "Harvest bots";
            this.btnHarvestBots.UseVisualStyleBackColor = true;
            this.btnHarvestBots.Click += new System.EventHandler(this.btnHarvestBots_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 95);
            this.Controls.Add(this.btnHarvestBots);
            this.Controls.Add(this.btnAIBots);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAIBots;
        private System.Windows.Forms.Button btnHarvestBots;
    }
}