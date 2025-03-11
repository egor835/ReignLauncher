namespace RCRL
{
    partial class LauncherForm
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherForm));
            label1 = new Label();
            cbVersion = new ComboBox();
            pbFiles = new ProgressBar();
            lbProgress = new Label();
            btnStart = new Button();
            usernameInput = new TextBox();
            label2 = new Label();
            eventTimer = new System.Windows.Forms.Timer(components);
            closeBtn = new Label();
            settingsBtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Minecraft Seven v2", 14.2499981F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(210, 418);
            label1.Name = "label1";
            label1.Size = new Size(76, 17);
            label1.TabIndex = 0;
            label1.Text = "Cборка:\r\n";
            // 
            // cbVersion
            // 
            cbVersion.FormattingEnabled = true;
            cbVersion.Location = new Point(292, 419);
            cbVersion.Name = "cbVersion";
            cbVersion.Size = new Size(213, 23);
            cbVersion.TabIndex = 1;
            // 
            // pbFiles
            // 
            pbFiles.Location = new Point(12, 448);
            pbFiles.Name = "pbFiles";
            pbFiles.Size = new Size(674, 23);
            pbFiles.TabIndex = 10;
            // 
            // lbProgress
            // 
            lbProgress.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbProgress.AutoSize = true;
            lbProgress.BackColor = Color.Transparent;
            lbProgress.ForeColor = Color.White;
            lbProgress.Location = new Point(12, 474);
            lbProgress.Name = "lbProgress";
            lbProgress.Size = new Size(44, 15);
            lbProgress.TabIndex = 5;
            lbProgress.Text = "Ready?";
            lbProgress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnStart.BackColor = Color.DarkOliveGreen;
            btnStart.Font = new Font("Minecraft Seven v2", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnStart.Image = (Image)resources.GetObject("btnStart.Image");
            btnStart.Location = new Point(12, 368);
            btnStart.Margin = new Padding(0);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(191, 76);
            btnStart.TabIndex = 7;
            btnStart.Text = "ЗАПУСК";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // usernameInput
            // 
            usernameInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            usernameInput.Location = new Point(292, 390);
            usernameInput.Name = "usernameInput";
            usernameInput.Size = new Size(215, 23);
            usernameInput.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Minecraft Seven v2", 14.2499981F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(243, 390);
            label2.Name = "label2";
            label2.Size = new Size(43, 17);
            label2.TabIndex = 8;
            label2.Text = "Ник:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // eventTimer
            // 
            eventTimer.Enabled = true;
            eventTimer.Tick += eventTimer_Tick;
            // 
            // closeBtn
            // 
            closeBtn.AutoSize = true;
            closeBtn.BackColor = Color.Transparent;
            closeBtn.Font = new Font("Minecraft", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            closeBtn.ForeColor = Color.White;
            closeBtn.Location = new Point(665, 9);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(21, 23);
            closeBtn.TabIndex = 14;
            closeBtn.Text = "X";
            closeBtn.Click += closeBtn_Click;
            // 
            // settingsBtn
            // 
            settingsBtn.Location = new Point(589, 377);
            settingsBtn.Name = "settingsBtn";
            settingsBtn.Size = new Size(64, 64);
            settingsBtn.TabIndex = 15;
            settingsBtn.Text = "l";
            settingsBtn.UseVisualStyleBackColor = true;
            settingsBtn.MouseEnter += settingsBtn_Hover;
            settingsBtn.MouseLeave += settingsBtn_noHover;
            settingsBtn.MouseDown += settingsBtn_press;
            settingsBtn.MouseUp += settingsBtn_release;
            // 
            // LauncherForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Lime;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(700, 500);
            ControlBox = false;
            Controls.Add(settingsBtn);
            Controls.Add(closeBtn);
            Controls.Add(usernameInput);
            Controls.Add(label2);
            Controls.Add(btnStart);
            Controls.Add(lbProgress);
            Controls.Add(pbFiles);
            Controls.Add(cbVersion);
            Controls.Add(label1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(700, 500);
            MinimumSize = new Size(700, 500);
            Name = "LauncherForm";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.CenterScreen;
            TransparencyKey = Color.Lime;
            Load += LauncherForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cbVersion;
        private ProgressBar pbFiles;
        private Label lbProgress;
        private Button btnStart;
        private TextBox usernameInput;
        private Label label2;
        private System.Windows.Forms.Timer eventTimer;
        
        private Label closeBtn;
        private Button settingsBtn;
    }
}
