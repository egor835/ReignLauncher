﻿using System.Drawing.Text;
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
            btnStart = new PictureBox();
            usernameInput = new TextBox();
            eventTimer = new System.Windows.Forms.Timer(components);
            settingsBtn = new PictureBox();
            folderBtn = new PictureBox();
            NewsLabel = new Label();
            logo = new PictureBox();
            crown = new PictureBox();
            closeBtn = new PictureBox();
            nickIMG = new PictureBox();
            buildsIMG = new PictureBox();
            box1 = new PictureBox();
            box2 = new PictureBox();
            NewsRTB = new Label();
            sunflower = new PictureBox();
            book = new PictureBox();
            easterLabel = new Label();
            hideBtn = new PictureBox();
            UpdateBox = new Label();
            PackUpdateBox = new Label();
            VersionBox = new Label();
            ((System.ComponentModel.ISupportInitialize)btnStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)settingsBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)folderBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)logo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)crown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)closeBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nickIMG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)buildsIMG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)box1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)box2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sunflower).BeginInit();
            ((System.ComponentModel.ISupportInitialize)book).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hideBtn).BeginInit();
            SuspendLayout();
            // 
            // cbVersion
            // 
            cbVersion.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVersion.FlatStyle = FlatStyle.Flat;
            cbVersion.Font = new Font("Calibri", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            cbVersion.Location = new Point(77, 746);
            cbVersion.Name = "cbVersion";
            cbVersion.Size = new Size(261, 27);
            cbVersion.TabIndex = 1;
            cbVersion.SelectedIndexChanged += cbVersion_SelectedIndexChanged;
            // 
            // pbFiles
            // 
            pbFiles.Location = new Point(757, 654);
            pbFiles.Name = "pbFiles";
            pbFiles.Size = new Size(423, 16);
            pbFiles.TabIndex = 10;
            pbFiles.Visible = false;
            // 
            // lbProgress
            // 
            lbProgress.AutoSize = true;
            lbProgress.BackColor = Color.Transparent;
            lbProgress.Font = new Font("Calibri", 19F, FontStyle.Regular, GraphicsUnit.Pixel);
            lbProgress.ForeColor = Color.White;
            lbProgress.Location = new Point(752, 628);
            lbProgress.Margin = new Padding(0);
            lbProgress.Name = "lbProgress";
            lbProgress.Size = new Size(14, 23);
            lbProgress.TabIndex = 5;
            lbProgress.Text = " ";
            lbProgress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.Transparent;
            btnStart.BackgroundImage = Properties.Resources.Play;
            btnStart.BackgroundImageLayout = ImageLayout.Stretch;
            btnStart.ForeColor = Color.Transparent;
            btnStart.Location = new Point(850, 680);
            btnStart.Margin = new Padding(0);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(330, 100);
            btnStart.TabIndex = 7;
            btnStart.TabStop = false;
            btnStart.MouseDown += btnStart_press;
            btnStart.MouseEnter += btnStart_Hover;
            btnStart.MouseLeave += btnStart_noHover;
            btnStart.MouseUp += btnStart_release;
            // 
            // usernameInput
            // 
            usernameInput.BorderStyle = BorderStyle.None;
            usernameInput.Font = new Font("Calibri", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            usernameInput.Location = new Point(80, 689);
            usernameInput.Name = "usernameInput";
            usernameInput.PlaceholderText = "Введите никнейм";
            usernameInput.Size = new Size(261, 20);
            usernameInput.TabIndex = 9;
            usernameInput.TextChanged += usernameInput_TextChanged;
            usernameInput.KeyDown += usernameInput_KeyDown;
            // 
            // eventTimer
            // 
            eventTimer.Tick += eventTimer_Tick;
            // 
            // settingsBtn
            // 
            settingsBtn.BackColor = Color.Transparent;
            settingsBtn.BackgroundImage = Properties.Resources.Settings;
            settingsBtn.BackgroundImageLayout = ImageLayout.Stretch;
            settingsBtn.ForeColor = Color.Transparent;
            settingsBtn.Location = new Point(757, 702);
            settingsBtn.Margin = new Padding(0);
            settingsBtn.Name = "settingsBtn";
            settingsBtn.Size = new Size(56, 56);
            settingsBtn.TabIndex = 16;
            settingsBtn.TabStop = false;
            settingsBtn.Click += settingsBtn_Click;
            settingsBtn.MouseEnter += settingsBtn_Hover;
            settingsBtn.MouseLeave += settingsBtn_noHover;
            // 
            // folderBtn
            // 
            folderBtn.BackColor = Color.Transparent;
            folderBtn.BackgroundImage = Properties.Resources.Mods;
            folderBtn.BackgroundImageLayout = ImageLayout.Stretch;
            folderBtn.ForeColor = Color.Transparent;
            folderBtn.Location = new Point(387, 702);
            folderBtn.Margin = new Padding(0);
            folderBtn.Name = "folderBtn";
            folderBtn.Size = new Size(56, 56);
            folderBtn.TabIndex = 15;
            folderBtn.TabStop = false;
            folderBtn.Click += folderBtn_Click;
            folderBtn.MouseEnter += folderBtn_Hover;
            folderBtn.MouseLeave += folderBtn_noHover;
            // 
            // NewsLabel
            // 
            NewsLabel.AutoSize = true;
            NewsLabel.BackColor = Color.Transparent;
            NewsLabel.Font = new Font("Calibri", 48F, FontStyle.Regular, GraphicsUnit.Pixel);
            NewsLabel.ForeColor = Color.FromArgb(101, 166, 106);
            NewsLabel.Location = new Point(36, 150);
            NewsLabel.Name = "NewsLabel";
            NewsLabel.Size = new Size(595, 59);
            NewsLabel.TabIndex = 17;
            NewsLabel.Text = "Нет соединения с сервером";
            // 
            // logo
            // 
            logo.BackColor = Color.Transparent;
            logo.BackgroundImage = Properties.Resources.Logo;
            logo.BackgroundImageLayout = ImageLayout.Stretch;
            logo.Enabled = false;
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
            crown.BackgroundImageLayout = ImageLayout.Stretch;
            crown.Location = new Point(480, 628);
            crown.Margin = new Padding(0);
            crown.Name = "crown";
            crown.Size = new Size(240, 171);
            crown.TabIndex = 19;
            crown.TabStop = false;
            // 
            // closeBtn
            // 
            closeBtn.BackColor = Color.Transparent;
            closeBtn.BackgroundImage = Properties.Resources.Exit;
            closeBtn.BackgroundImageLayout = ImageLayout.Stretch;
            closeBtn.ForeColor = Color.Transparent;
            closeBtn.Location = new Point(1140, 0);
            closeBtn.Margin = new Padding(0);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(60, 60);
            closeBtn.TabIndex = 20;
            closeBtn.TabStop = false;
            closeBtn.Click += closeBtn_Click;
            closeBtn.MouseEnter += closeBtn_Hover;
            closeBtn.MouseLeave += closeBtn_noHover;
            // 
            // nickIMG
            // 
            nickIMG.BackColor = Color.Transparent;
            nickIMG.BackgroundImage = Properties.Resources.Icon_nickname;
            nickIMG.BackgroundImageLayout = ImageLayout.Stretch;
            nickIMG.Location = new Point(20, 680);
            nickIMG.Name = "nickIMG";
            nickIMG.Size = new Size(40, 40);
            nickIMG.TabIndex = 22;
            nickIMG.TabStop = false;
            // 
            // buildsIMG
            // 
            buildsIMG.BackColor = Color.Transparent;
            buildsIMG.BackgroundImage = Properties.Resources.Icon_builds;
            buildsIMG.BackgroundImageLayout = ImageLayout.Stretch;
            buildsIMG.Location = new Point(20, 740);
            buildsIMG.Name = "buildsIMG";
            buildsIMG.Size = new Size(40, 40);
            buildsIMG.TabIndex = 23;
            buildsIMG.TabStop = false;
            // 
            // box1
            // 
            box1.BackColor = Color.Transparent;
            box1.BackgroundImage = Properties.Resources.box;
            box1.BackgroundImageLayout = ImageLayout.Stretch;
            box1.Location = new Point(65, 680);
            box1.Name = "box1";
            box1.Size = new Size(285, 40);
            box1.TabIndex = 24;
            box1.TabStop = false;
            // 
            // box2
            // 
            box2.BackColor = Color.Transparent;
            box2.BackgroundImage = Properties.Resources.box;
            box2.BackgroundImageLayout = ImageLayout.Stretch;
            box2.Location = new Point(65, 740);
            box2.Name = "box2";
            box2.Size = new Size(285, 40);
            box2.TabIndex = 25;
            box2.TabStop = false;
            // 
            // NewsRTB
            // 
            NewsRTB.BackColor = Color.Transparent;
            NewsRTB.Font = new Font("Calibri", 24F, FontStyle.Regular, GraphicsUnit.Pixel);
            NewsRTB.ForeColor = Color.White;
            NewsRTB.Location = new Point(77, 243);
            NewsRTB.Name = "NewsRTB";
            NewsRTB.Size = new Size(643, 385);
            NewsRTB.TabIndex = 26;
            NewsRTB.Text = "Но если сборка уже установлена, то вы можете начать играть :3";
            // 
            // sunflower
            // 
            sunflower.BackColor = Color.Transparent;
            sunflower.BackgroundImage = Properties.Resources.Sunflower;
            sunflower.BackgroundImageLayout = ImageLayout.Stretch;
            sunflower.ForeColor = Color.Transparent;
            sunflower.Location = new Point(20, 20);
            sunflower.Margin = new Padding(0);
            sunflower.Name = "sunflower";
            sunflower.Size = new Size(64, 64);
            sunflower.TabIndex = 27;
            sunflower.TabStop = false;
            sunflower.Click += sunflower_Click;
            sunflower.MouseEnter += sunflower_Hover;
            sunflower.MouseLeave += sunflower_noHover;
            // 
            // book
            // 
            book.BackColor = Color.Transparent;
            book.BackgroundImage = Properties.Resources.Textbook;
            book.BackgroundImageLayout = ImageLayout.Stretch;
            book.ForeColor = Color.Transparent;
            book.Location = new Point(96, 20);
            book.Margin = new Padding(0);
            book.Name = "book";
            book.Size = new Size(64, 64);
            book.TabIndex = 28;
            book.TabStop = false;
            book.Click += book_Click;
            book.MouseEnter += book_Hover;
            book.MouseLeave += book_noHover;
            // 
            // easterLabel
            // 
            easterLabel.AutoSize = true;
            easterLabel.BackColor = Color.Transparent;
            easterLabel.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            easterLabel.ForeColor = Color.Green;
            easterLabel.Location = new Point(65, 663);
            easterLabel.Name = "easterLabel";
            easterLabel.Size = new Size(10, 14);
            easterLabel.TabIndex = 29;
            easterLabel.Text = " ";
            // 
            // hideBtn
            // 
            hideBtn.BackColor = Color.Transparent;
            hideBtn.BackgroundImage = Properties.Resources.hide;
            hideBtn.BackgroundImageLayout = ImageLayout.Stretch;
            hideBtn.ForeColor = Color.Transparent;
            hideBtn.Location = new Point(1080, 0);
            hideBtn.Margin = new Padding(0);
            hideBtn.Name = "hideBtn";
            hideBtn.Size = new Size(60, 60);
            hideBtn.TabIndex = 30;
            hideBtn.TabStop = false;
            hideBtn.Click += hideBtn_Click;
            hideBtn.MouseEnter += hideBtn_Hover;
            hideBtn.MouseLeave += hideBtn_noHover;
            // 
            // UpdateBox
            // 
            UpdateBox.BackColor = Color.Transparent;
            UpdateBox.Font = new Font("Calibri", 24F, FontStyle.Regular, GraphicsUnit.Pixel);
            UpdateBox.ForeColor = Color.Gold;
            UpdateBox.Location = new Point(411, 95);
            UpdateBox.Name = "UpdateBox";
            UpdateBox.Size = new Size(378, 55);
            UpdateBox.TabIndex = 32;
            UpdateBox.Text = "Доступно обновление лаунчера!";
            UpdateBox.TextAlign = ContentAlignment.MiddleCenter;
            UpdateBox.Click += UpdateBox_Click;
            // 
            // PackUpdateBox
            // 
            PackUpdateBox.BackColor = Color.Transparent;
            PackUpdateBox.Font = new Font("Calibri", 18F, FontStyle.Regular, GraphicsUnit.Pixel);
            PackUpdateBox.ForeColor = Color.Gold;
            PackUpdateBox.Location = new Point(850, 654);
            PackUpdateBox.Name = "PackUpdateBox";
            PackUpdateBox.Size = new Size(330, 26);
            PackUpdateBox.TabIndex = 33;
            PackUpdateBox.Text = "Есть обновления сборки";
            PackUpdateBox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VersionBox
            // 
            VersionBox.BackColor = Color.Transparent;
            VersionBox.Font = new Font("Calibri", 18F, FontStyle.Regular, GraphicsUnit.Pixel);
            VersionBox.ForeColor = Color.White;
            VersionBox.Location = new Point(244, 41);
            VersionBox.Name = "VersionBox";
            VersionBox.Size = new Size(124, 43);
            VersionBox.TabIndex = 31;
            VersionBox.Text = "0.0.0";
            // 
            // LauncherForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1200, 800);
            ControlBox = false;
            Controls.Add(PackUpdateBox);
            Controls.Add(lbProgress);
            Controls.Add(pbFiles);
            Controls.Add(UpdateBox);
            Controls.Add(hideBtn);
            Controls.Add(easterLabel);
            Controls.Add(book);
            Controls.Add(sunflower);
            Controls.Add(NewsRTB);
            Controls.Add(cbVersion);
            Controls.Add(box2);
            Controls.Add(usernameInput);
            Controls.Add(box1);
            Controls.Add(buildsIMG);
            Controls.Add(nickIMG);
            Controls.Add(closeBtn);
            Controls.Add(crown);
            Controls.Add(logo);
            Controls.Add(NewsLabel);
            Controls.Add(folderBtn);
            Controls.Add(settingsBtn);
            Controls.Add(btnStart);
            Controls.Add(VersionBox);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1200, 800);
            MinimizeBox = false;
            MinimumSize = new Size(1200, 800);
            Name = "LauncherForm";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ReignCraft Launcher";
            Load += LauncherForm_Load;
            ((System.ComponentModel.ISupportInitialize)btnStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)settingsBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)folderBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)logo).EndInit();
            ((System.ComponentModel.ISupportInitialize)crown).EndInit();
            ((System.ComponentModel.ISupportInitialize)closeBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)nickIMG).EndInit();
            ((System.ComponentModel.ISupportInitialize)buildsIMG).EndInit();
            ((System.ComponentModel.ISupportInitialize)box1).EndInit();
            ((System.ComponentModel.ISupportInitialize)box2).EndInit();
            ((System.ComponentModel.ISupportInitialize)sunflower).EndInit();
            ((System.ComponentModel.ISupportInitialize)book).EndInit();
            ((System.ComponentModel.ISupportInitialize)hideBtn).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cbVersion;
        private ProgressBar pbFiles;
        private Label lbProgress;
        private PictureBox btnStart;
        private TextBox usernameInput;
        private System.Windows.Forms.Timer eventTimer;
        private PictureBox settingsBtn;
        private PictureBox folderBtn;
        private Label NewsLabel;
        private PictureBox logo;
        private PictureBox crown;
        private PictureBox closeBtn;
        private PictureBox nickIMG;
        private PictureBox buildsIMG;
        private PictureBox box1;
        private PictureBox box2;
        private Label NewsRTB;
        private PictureBox sunflower;
        private PictureBox book;
        private Label easterLabel;
        private PictureBox hideBtn;
        private Label UpdateBox;
        private Label PackUpdateBox;
        private Label VersionBox;
    }
}
