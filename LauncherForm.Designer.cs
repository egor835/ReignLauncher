using System.Windows.Forms;

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
            cbVersion = new ComboBox();
            pbFiles = new ProgressBar();
            lbProgress = new Label();
            btnStart = new Button();
            usernameInput = new TextBox();
            eventTimer = new System.Windows.Forms.Timer(components);
            closeBtn = new Label();
            settingsBtn = new Button();
            folderBtn = new Button();
            NewsLabel = new Label();
            logo = new PictureBox();
            crown = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)logo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)crown).BeginInit();
            SuspendLayout();
            // 
            // cbVersion
            // 
            cbVersion.FormattingEnabled = true;
            cbVersion.Location = new Point(123, 554);
            cbVersion.Name = "cbVersion";
            cbVersion.Size = new Size(213, 23);
            cbVersion.TabIndex = 1;
            // 
            // pbFiles
            // 
            pbFiles.Location = new Point(46, 322);
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
            lbProgress.Location = new Point(46, 304);
            lbProgress.Name = "lbProgress";
            lbProgress.Size = new Size(44, 15);
            lbProgress.TabIndex = 5;
            lbProgress.Text = "Ready?";
            lbProgress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnStart.BackColor = Color.Transparent;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnStart.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.ForeColor = Color.Transparent;
            btnStart.Image = Properties.Resources.Play;
            btnStart.Location = new Point(845, 675);
            btnStart.Margin = new Padding(0);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(335, 105);
            btnStart.TabIndex = 7;
            btnStart.UseVisualStyleBackColor = false;
            btnStart.MouseDown += btnStart_press;
            btnStart.MouseEnter += btnStart_Hover;
            btnStart.MouseLeave += btnStart_noHover;
            btnStart.MouseUp += btnStart_release;
            // 
            // usernameInput
            // 
            usernameInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            usernameInput.Location = new Point(123, 525);
            usernameInput.Name = "usernameInput";
            usernameInput.Size = new Size(229, 23);
            usernameInput.TabIndex = 9;
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
            closeBtn.Location = new Point(1167, 9);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(21, 23);
            closeBtn.TabIndex = 14;
            closeBtn.Text = "X";
            closeBtn.Click += closeBtn_Click;
            // 
            // settingsBtn
            // 
            settingsBtn.BackColor = Color.Transparent;
            settingsBtn.FlatAppearance.BorderSize = 0;
            settingsBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            settingsBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            settingsBtn.FlatStyle = FlatStyle.Flat;
            settingsBtn.ForeColor = Color.Transparent;
            settingsBtn.Image = Properties.Resources.Settings;
            settingsBtn.Location = new Point(760, 697);
            settingsBtn.Name = "settingsBtn";
            settingsBtn.Size = new Size(61, 61);
            settingsBtn.TabIndex = 16;
            settingsBtn.UseVisualStyleBackColor = false;
            settingsBtn.Click += settingsBtn_Click;
            settingsBtn.MouseEnter += settingsBtn_Hover;
            settingsBtn.MouseLeave += settingsBtn_noHover;
            // 
            // folderBtn
            // 
            folderBtn.BackColor = Color.Transparent;
            folderBtn.FlatAppearance.BorderSize = 0;
            folderBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            folderBtn.FlatStyle = FlatStyle.Flat;
            folderBtn.ForeColor = Color.Transparent;
            folderBtn.Image = Properties.Resources.Mods;
            folderBtn.Location = new Point(361, 697);
            folderBtn.Name = "folderBtn";
            folderBtn.Size = new Size(61, 61);
            folderBtn.TabIndex = 15;
            folderBtn.UseVisualStyleBackColor = false;
            folderBtn.Click += folderBtn_Click;
            folderBtn.MouseEnter += folderBtn_Hover;
            folderBtn.MouseLeave += folderBtn_noHover;
            // 
            // NewsLabel
            // 
            NewsLabel.AutoSize = true;
            NewsLabel.BackColor = Color.Transparent;
            NewsLabel.ForeColor = Color.White;
            NewsLabel.Location = new Point(159, 193);
            NewsLabel.Name = "NewsLabel";
            NewsLabel.Size = new Size(57, 15);
            NewsLabel.TabIndex = 17;
            NewsLabel.Text = "Новости:";
            // 
            // logo
            // 
            logo.BackColor = Color.Transparent;
            logo.BackgroundImage = Properties.Resources.Logo;
            logo.Location = new Point(411, 20);
            logo.Name = "logo";
            logo.Size = new Size(378, 72);
            logo.TabIndex = 18;
            logo.TabStop = false;
            // 
            // crown
            // 
            crown.BackColor = Color.Transparent;
            crown.BackgroundImage = Properties.Resources.Crown;
            crown.Location = new Point(480, 628);
            crown.Name = "crown";
            crown.Size = new Size(240, 171);
            crown.TabIndex = 19;
            crown.TabStop = false;
            // 
            // LauncherForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Lime;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1200, 800);
            ControlBox = false;
            Controls.Add(crown);
            Controls.Add(logo);
            Controls.Add(NewsLabel);
            Controls.Add(folderBtn);
            Controls.Add(settingsBtn);
            Controls.Add(closeBtn);
            Controls.Add(usernameInput);
            Controls.Add(btnStart);
            Controls.Add(lbProgress);
            Controls.Add(pbFiles);
            Controls.Add(cbVersion);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(1200, 800);
            MinimumSize = new Size(1200, 800);
            Name = "LauncherForm";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ReignCraft Launcher";
            TransparencyKey = Color.Lime;
            Load += LauncherForm_Load;
            ((System.ComponentModel.ISupportInitialize)logo).EndInit();
            ((System.ComponentModel.ISupportInitialize)crown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cbVersion;
        private ProgressBar pbFiles;
        private Label lbProgress;
        private Button btnStart;
        private TextBox usernameInput;
        private System.Windows.Forms.Timer eventTimer;
        
        private Label closeBtn;
        private Button settingsBtn;
        private Button folderBtn;
        private Label NewsLabel;
        private PictureBox logo;
        private PictureBox crown;
    }
}
