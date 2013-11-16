namespace AIBots
{
    partial class MainForm<World, Bot, Settings>
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
            this.components = new System.ComponentModel.Container();
            System.Drawing.Drawing2D.ColorBlend colorBlend1 = new System.Drawing.Drawing2D.ColorBlend();
            System.Drawing.Drawing2D.ColorBlend colorBlend2 = new System.Drawing.Drawing2D.ColorBlend();
            System.Drawing.Drawing2D.ColorBlend colorBlend3 = new System.Drawing.Drawing2D.ColorBlend();
            this.picWorld = new System.Windows.Forms.PictureBox();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkEvolve = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.picHofSpread = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.graphAvg = new DashboardControls.Graph();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.graphMin = new DashboardControls.Graph();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.graphMax = new DashboardControls.Graph();
            this.chkShowTrails = new System.Windows.Forms.CheckBox();
            this.pgSettings = new System.Windows.Forms.PropertyGrid();
            this.lblCurrentShownWorld = new System.Windows.Forms.Label();
            this.btnCopyHistory = new System.Windows.Forms.Button();
            this.btnShowLatest = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.CheckBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblBotFitness = new System.Windows.Forms.Label();
            this.btnShowNetwork = new System.Windows.Forms.Button();
            this.chkAutoSelectBestPerforming = new System.Windows.Forms.CheckBox();
            this.chkDrawVision = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sldBot = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.picWorld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHofSpread)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldBot)).BeginInit();
            this.SuspendLayout();
            // 
            // picWorld
            // 
            this.picWorld.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picWorld.Location = new System.Drawing.Point(0, 0);
            this.picWorld.Name = "picWorld";
            this.picWorld.Size = new System.Drawing.Size(423, 632);
            this.picWorld.TabIndex = 0;
            this.picWorld.TabStop = false;
            this.picWorld.Paint += new System.Windows.Forms.PaintEventHandler(this.picWorld_Paint);
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Interval = 25;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.picWorld);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chkEvolve);
            this.splitContainer1.Panel2.Controls.Add(this.btnReset);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.chkShowTrails);
            this.splitContainer1.Panel2.Controls.Add(this.pgSettings);
            this.splitContainer1.Panel2.Controls.Add(this.lblCurrentShownWorld);
            this.splitContainer1.Panel2.Controls.Add(this.btnCopyHistory);
            this.splitContainer1.Panel2.Controls.Add(this.btnShowLatest);
            this.splitContainer1.Panel2.Controls.Add(this.lblStatus);
            this.splitContainer1.Panel2.Controls.Add(this.btnRun);
            this.splitContainer1.Size = new System.Drawing.Size(701, 632);
            this.splitContainer1.SplitterDistance = 423;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // chkEvolve
            // 
            this.chkEvolve.AutoSize = true;
            this.chkEvolve.Location = new System.Drawing.Point(182, 239);
            this.chkEvolve.Name = "chkEvolve";
            this.chkEvolve.Size = new System.Drawing.Size(59, 17);
            this.chkEvolve.TabIndex = 11;
            this.chkEvolve.Text = "Evolve";
            this.chkEvolve.UseVisualStyleBackColor = true;
            this.chkEvolve.CheckedChanged += new System.EventHandler(this.chkEvolve_CheckedChanged);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(152, 41);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(119, 23);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.picHofSpread);
            this.groupBox4.Location = new System.Drawing.Point(7, 314);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 60);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Hall of fame genome spread";
            // 
            // picHofSpread
            // 
            this.picHofSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picHofSpread.Location = new System.Drawing.Point(3, 16);
            this.picHofSpread.Name = "picHofSpread";
            this.picHofSpread.Size = new System.Drawing.Size(254, 41);
            this.picHofSpread.TabIndex = 0;
            this.picHofSpread.TabStop = false;
            this.picHofSpread.Paint += new System.Windows.Forms.PaintEventHandler(this.picHofSpread_Paint);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.graphAvg);
            this.groupBox3.Location = new System.Drawing.Point(7, 540);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(260, 80);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Avg fitness";
            // 
            // graphAvg
            // 
            this.graphAvg.AverageColor = System.Drawing.Color.Blue;
            this.graphAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphAvg.Fill = false;
            this.graphAvg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphAvg.FormatString = "{0:N2}";
            this.graphAvg.GridColor = System.Drawing.Color.LightGray;
            this.graphAvg.GridSize = 3;
            colorBlend1.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Salmon,
        System.Drawing.Color.Lime};
            colorBlend1.Positions = new float[] {
        0F,
        1F};
            this.graphAvg.LineColor = colorBlend1;
            this.graphAvg.Location = new System.Drawing.Point(3, 16);
            this.graphAvg.Name = "graphAvg";
            this.graphAvg.ShowAverage = false;
            this.graphAvg.Size = new System.Drawing.Size(253, 61);
            this.graphAvg.TabIndex = 6;
            this.graphAvg.Values = new double[0];
            this.graphAvg.ValueUnit = null;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.graphMin);
            this.groupBox2.Location = new System.Drawing.Point(7, 460);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Min fitness";
            // 
            // graphMin
            // 
            this.graphMin.AverageColor = System.Drawing.Color.Blue;
            this.graphMin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphMin.Fill = false;
            this.graphMin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphMin.FormatString = "{0:N2}";
            this.graphMin.GridColor = System.Drawing.Color.LightGray;
            this.graphMin.GridSize = 3;
            colorBlend2.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Salmon,
        System.Drawing.Color.Lime};
            colorBlend2.Positions = new float[] {
        0F,
        1F};
            this.graphMin.LineColor = colorBlend2;
            this.graphMin.Location = new System.Drawing.Point(3, 16);
            this.graphMin.Name = "graphMin";
            this.graphMin.ShowAverage = false;
            this.graphMin.Size = new System.Drawing.Size(253, 61);
            this.graphMin.TabIndex = 5;
            this.graphMin.Values = new double[0];
            this.graphMin.ValueUnit = null;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.graphMax);
            this.groupBox1.Location = new System.Drawing.Point(7, 380);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 80);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Max fitness";
            // 
            // graphMax
            // 
            this.graphMax.AverageColor = System.Drawing.Color.Blue;
            this.graphMax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphMax.Fill = false;
            this.graphMax.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphMax.FormatString = "{0:N2}";
            this.graphMax.GridColor = System.Drawing.Color.LightGray;
            this.graphMax.GridSize = 3;
            colorBlend3.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Salmon,
        System.Drawing.Color.Lime};
            colorBlend3.Positions = new float[] {
        0F,
        1F};
            this.graphMax.LineColor = colorBlend3;
            this.graphMax.Location = new System.Drawing.Point(3, 16);
            this.graphMax.Name = "graphMax";
            this.graphMax.ShowAverage = false;
            this.graphMax.Size = new System.Drawing.Size(253, 61);
            this.graphMax.TabIndex = 0;
            this.graphMax.Values = new double[0];
            this.graphMax.ValueUnit = null;
            // 
            // chkShowTrails
            // 
            this.chkShowTrails.AutoSize = true;
            this.chkShowTrails.Location = new System.Drawing.Point(182, 216);
            this.chkShowTrails.Name = "chkShowTrails";
            this.chkShowTrails.Size = new System.Drawing.Size(77, 17);
            this.chkShowTrails.TabIndex = 8;
            this.chkShowTrails.Text = "Show trails";
            this.chkShowTrails.UseVisualStyleBackColor = true;
            // 
            // pgSettings
            // 
            this.pgSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgSettings.HelpVisible = false;
            this.pgSettings.Location = new System.Drawing.Point(2, 41);
            this.pgSettings.Name = "pgSettings";
            this.pgSettings.Size = new System.Drawing.Size(272, 169);
            this.pgSettings.TabIndex = 7;
            // 
            // lblCurrentShownWorld
            // 
            this.lblCurrentShownWorld.AutoSize = true;
            this.lblCurrentShownWorld.Location = new System.Drawing.Point(4, 222);
            this.lblCurrentShownWorld.Name = "lblCurrentShownWorld";
            this.lblCurrentShownWorld.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentShownWorld.TabIndex = 4;
            // 
            // btnCopyHistory
            // 
            this.btnCopyHistory.Location = new System.Drawing.Point(152, 12);
            this.btnCopyHistory.Name = "btnCopyHistory";
            this.btnCopyHistory.Size = new System.Drawing.Size(119, 23);
            this.btnCopyHistory.TabIndex = 3;
            this.btnCopyHistory.Text = "Copy history as csv";
            this.btnCopyHistory.UseVisualStyleBackColor = true;
            this.btnCopyHistory.Click += new System.EventHandler(this.btnCopyHistory_Click);
            // 
            // btnShowLatest
            // 
            this.btnShowLatest.Location = new System.Drawing.Point(45, 12);
            this.btnShowLatest.Name = "btnShowLatest";
            this.btnShowLatest.Size = new System.Drawing.Size(101, 23);
            this.btnShowLatest.TabIndex = 2;
            this.btnShowLatest.Text = "Show latest world";
            this.btnShowLatest.UseVisualStyleBackColor = true;
            this.btnShowLatest.Click += new System.EventHandler(this.btnEvolve_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(4, 244);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(267, 61);
            this.lblStatus.TabIndex = 1;
            // 
            // btnRun
            // 
            this.btnRun.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnRun.AutoSize = true;
            this.btnRun.Location = new System.Drawing.Point(2, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(37, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.CheckedChanged += new System.EventHandler(this.btnRun_CheckedChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblBotFitness);
            this.splitContainer2.Panel2.Controls.Add(this.btnShowNetwork);
            this.splitContainer2.Panel2.Controls.Add(this.chkAutoSelectBestPerforming);
            this.splitContainer2.Panel2.Controls.Add(this.chkDrawVision);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.sldBot);
            this.splitContainer2.Size = new System.Drawing.Size(701, 673);
            this.splitContainer2.SplitterDistance = 632;
            this.splitContainer2.TabIndex = 2;
            this.splitContainer2.TabStop = false;
            // 
            // lblBotFitness
            // 
            this.lblBotFitness.AutoSize = true;
            this.lblBotFitness.Location = new System.Drawing.Point(211, 12);
            this.lblBotFitness.Name = "lblBotFitness";
            this.lblBotFitness.Size = new System.Drawing.Size(73, 13);
            this.lblBotFitness.TabIndex = 5;
            this.lblBotFitness.Text = "Fitness: 99.99";
            // 
            // btnShowNetwork
            // 
            this.btnShowNetwork.Location = new System.Drawing.Point(598, 7);
            this.btnShowNetwork.Name = "btnShowNetwork";
            this.btnShowNetwork.Size = new System.Drawing.Size(91, 23);
            this.btnShowNetwork.TabIndex = 4;
            this.btnShowNetwork.Text = "Show network";
            this.btnShowNetwork.UseVisualStyleBackColor = true;
            this.btnShowNetwork.Click += new System.EventHandler(this.btnShowNetwork_Click);
            // 
            // chkAutoSelectBestPerforming
            // 
            this.chkAutoSelectBestPerforming.AutoSize = true;
            this.chkAutoSelectBestPerforming.Location = new System.Drawing.Point(406, 10);
            this.chkAutoSelectBestPerforming.Name = "chkAutoSelectBestPerforming";
            this.chkAutoSelectBestPerforming.Size = new System.Drawing.Size(172, 17);
            this.chkAutoSelectBestPerforming.TabIndex = 3;
            this.chkAutoSelectBestPerforming.Text = "Auto select best performing bot";
            this.chkAutoSelectBestPerforming.UseVisualStyleBackColor = true;
            // 
            // chkDrawVision
            // 
            this.chkDrawVision.AutoSize = true;
            this.chkDrawVision.Location = new System.Drawing.Point(312, 10);
            this.chkDrawVision.Name = "chkDrawVision";
            this.chkDrawVision.Size = new System.Drawing.Size(99, 17);
            this.chkDrawVision.TabIndex = 2;
            this.chkDrawVision.Text = "Draw bot vision";
            this.chkDrawVision.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select bot";
            // 
            // sldBot
            // 
            this.sldBot.AutoSize = false;
            this.sldBot.Location = new System.Drawing.Point(64, 7);
            this.sldBot.Name = "sldBot";
            this.sldBot.Size = new System.Drawing.Size(152, 23);
            this.sldBot.TabIndex = 0;
            this.sldBot.ValueChanged += new System.EventHandler(this.sldBot_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 673);
            this.Controls.Add(this.splitContainer2);
            this.Name = "MainForm";
            this.Text = "AI Bots";
            ((System.ComponentModel.ISupportInitialize)(this.picWorld)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHofSpread)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sldBot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picWorld;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox btnRun;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnShowLatest;
        private System.Windows.Forms.Button btnCopyHistory;
        private System.Windows.Forms.Label lblCurrentShownWorld;
        private DashboardControls.Graph  graphMax;
        private DashboardControls.Graph graphAvg;
        private DashboardControls.Graph graphMin;
        private System.Windows.Forms.PropertyGrid pgSettings;
        private System.Windows.Forms.CheckBox chkShowTrails;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox picHofSpread;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox chkEvolve;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar sldBot;
        private System.Windows.Forms.CheckBox chkDrawVision;
        private System.Windows.Forms.CheckBox chkAutoSelectBestPerforming;
        private System.Windows.Forms.Button btnShowNetwork;
        private System.Windows.Forms.Label lblBotFitness;
    }
}

