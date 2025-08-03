namespace FitnessTrackerSchool
{
    partial class Goal
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.gbGoal = new System.Windows.Forms.GroupBox();
            this.numeTargetGoal = new System.Windows.Forms.NumericUpDown();
            this.dtpGoalDate = new System.Windows.Forms.DateTimePicker();
            this.btnSaveGoal = new System.Windows.Forms.Button();
            this.btnCancelGoal = new System.Windows.Forms.Button();
            this.lblConfirmPwd = new System.Windows.Forms.Label();
            this.lblGoalDate = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.gbGoal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeTargetGoal)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(983, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 31);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.logoutToolStripMenuItem.Text = "Log&out";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(58, 28);
            this.toolStripLabel1.Text = "Activity";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // gbGoal
            // 
            this.gbGoal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGoal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gbGoal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbGoal.Controls.Add(this.numeTargetGoal);
            this.gbGoal.Controls.Add(this.dtpGoalDate);
            this.gbGoal.Controls.Add(this.btnSaveGoal);
            this.gbGoal.Controls.Add(this.btnCancelGoal);
            this.gbGoal.Controls.Add(this.lblConfirmPwd);
            this.gbGoal.Controls.Add(this.lblGoalDate);
            this.gbGoal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGoal.Location = new System.Drawing.Point(101, 54);
            this.gbGoal.Name = "gbGoal";
            this.gbGoal.Size = new System.Drawing.Size(780, 430);
            this.gbGoal.TabIndex = 5;
            this.gbGoal.TabStop = false;
            // 
            // numeTargetGoal
            // 
            this.numeTargetGoal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.numeTargetGoal.Location = new System.Drawing.Point(406, 195);
            this.numeTargetGoal.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numeTargetGoal.Name = "numeTargetGoal";
            this.numeTargetGoal.Size = new System.Drawing.Size(200, 30);
            this.numeTargetGoal.TabIndex = 2;
            // 
            // dtpGoalDate
            // 
            this.dtpGoalDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dtpGoalDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpGoalDate.Location = new System.Drawing.Point(406, 106);
            this.dtpGoalDate.Name = "dtpGoalDate";
            this.dtpGoalDate.Size = new System.Drawing.Size(200, 30);
            this.dtpGoalDate.TabIndex = 1;
            this.dtpGoalDate.Value = new System.DateTime(2025, 7, 25, 0, 0, 0, 0);
            // 
            // btnSaveGoal
            // 
            this.btnSaveGoal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSaveGoal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSaveGoal.BackColor = System.Drawing.Color.SkyBlue;
            this.btnSaveGoal.Location = new System.Drawing.Point(429, 302);
            this.btnSaveGoal.Name = "btnSaveGoal";
            this.btnSaveGoal.Size = new System.Drawing.Size(152, 61);
            this.btnSaveGoal.TabIndex = 4;
            this.btnSaveGoal.Text = "Save Goal";
            this.btnSaveGoal.UseVisualStyleBackColor = false;
            this.btnSaveGoal.Click += new System.EventHandler(this.btnSaveGoal_Click);
            // 
            // btnCancelGoal
            // 
            this.btnCancelGoal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancelGoal.BackColor = System.Drawing.Color.Coral;
            this.btnCancelGoal.Location = new System.Drawing.Point(240, 302);
            this.btnCancelGoal.Name = "btnCancelGoal";
            this.btnCancelGoal.Size = new System.Drawing.Size(152, 61);
            this.btnCancelGoal.TabIndex = 3;
            this.btnCancelGoal.Text = "Cancel";
            this.btnCancelGoal.UseVisualStyleBackColor = false;
            this.btnCancelGoal.Click += new System.EventHandler(this.btnCancelGoal_Click);
            // 
            // lblConfirmPwd
            // 
            this.lblConfirmPwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblConfirmPwd.AutoSize = true;
            this.lblConfirmPwd.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPwd.Location = new System.Drawing.Point(231, 195);
            this.lblConfirmPwd.Name = "lblConfirmPwd";
            this.lblConfirmPwd.Size = new System.Drawing.Size(144, 23);
            this.lblConfirmPwd.TabIndex = 5;
            this.lblConfirmPwd.Text = "Target Calories";
            // 
            // lblGoalDate
            // 
            this.lblGoalDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblGoalDate.AutoSize = true;
            this.lblGoalDate.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoalDate.Location = new System.Drawing.Point(231, 106);
            this.lblGoalDate.Name = "lblGoalDate";
            this.lblGoalDate.Size = new System.Drawing.Size(100, 23);
            this.lblGoalDate.TabIndex = 5;
            this.lblGoalDate.Text = "Goal Date";
            // 
            // Goal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FitnessTrackerSchool.Properties.Resources.fitness_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(983, 539);
            this.Controls.Add(this.gbGoal);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Goal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Goal";
            this.Load += new System.EventHandler(this.Goal_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbGoal.ResumeLayout(false);
            this.gbGoal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeTargetGoal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.GroupBox gbGoal;
        private System.Windows.Forms.DateTimePicker dtpGoalDate;
        private System.Windows.Forms.Button btnSaveGoal;
        private System.Windows.Forms.Button btnCancelGoal;
        private System.Windows.Forms.Label lblConfirmPwd;
        private System.Windows.Forms.Label lblGoalDate;
        private System.Windows.Forms.NumericUpDown numeTargetGoal;
    }
}