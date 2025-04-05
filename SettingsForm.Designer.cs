namespace RCRL
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            RAMLabel = new Label();
            useProxy = new CheckBox();
            faststartBox = new CheckBox();
            closeBtn = new PictureBox();
            okBtn = new PictureBox();
            ramBar = new TrackBar();
            resetBtn = new PictureBox();
            hcBtn = new CheckBox();
            dontresizeBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)closeBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)okBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ramBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)resetBtn).BeginInit();
            SuspendLayout();
            // 
            // RAMLabel
            // 
            resources.ApplyResources(RAMLabel, "RAMLabel");
            RAMLabel.BackColor = Color.Transparent;
            RAMLabel.ForeColor = Color.White;
            RAMLabel.Name = "RAMLabel";
            // 
            // useProxy
            // 
            useProxy.BackColor = Color.Transparent;
            resources.ApplyResources(useProxy, "useProxy");
            useProxy.ForeColor = Color.White;
            useProxy.Name = "useProxy";
            useProxy.UseVisualStyleBackColor = false;
            // 
            // faststartBox
            // 
            faststartBox.BackColor = Color.Transparent;
            resources.ApplyResources(faststartBox, "faststartBox");
            faststartBox.ForeColor = Color.White;
            faststartBox.Name = "faststartBox";
            faststartBox.UseVisualStyleBackColor = false;
            // 
            // closeBtn
            // 
            closeBtn.BackColor = Color.Transparent;
            closeBtn.BackgroundImage = Properties.Resources.Exit;
            resources.ApplyResources(closeBtn, "closeBtn");
            closeBtn.ForeColor = Color.Transparent;
            closeBtn.Name = "closeBtn";
            closeBtn.TabStop = false;
            closeBtn.Click += closeBtn_Click;
            closeBtn.MouseEnter += closeBtn_Hover;
            closeBtn.MouseLeave += closeBtn_noHover;
            // 
            // okBtn
            // 
            okBtn.BackColor = Color.Transparent;
            okBtn.BackgroundImage = Properties.Resources.Accept;
            resources.ApplyResources(okBtn, "okBtn");
            okBtn.Name = "okBtn";
            okBtn.TabStop = false;
            okBtn.MouseDown += okBtn_press;
            okBtn.MouseEnter += okBtn_Hover;
            okBtn.MouseLeave += okBtn_noHover;
            okBtn.MouseUp += okBtn_release;
            // 
            // ramBar
            // 
            ramBar.BackColor = Color.Black;
            resources.ApplyResources(ramBar, "ramBar");
            ramBar.Maximum = 16384;
            ramBar.Minimum = 1024;
            ramBar.Name = "ramBar";
            ramBar.Value = 4096;
            ramBar.ValueChanged += ramBar_change;
            // 
            // resetBtn
            // 
            resetBtn.BackColor = Color.Transparent;
            resetBtn.BackgroundImage = Properties.Resources.Reset;
            resources.ApplyResources(resetBtn, "resetBtn");
            resetBtn.Name = "resetBtn";
            resetBtn.TabStop = false;
            resetBtn.MouseDown += resetBtn_press;
            resetBtn.MouseEnter += resetBtn_Hover;
            resetBtn.MouseLeave += resetBtn_noHover;
            resetBtn.MouseUp += resetBtn_release;
            // 
            // hcBtn
            // 
            hcBtn.BackColor = Color.Transparent;
            resources.ApplyResources(hcBtn, "hcBtn");
            hcBtn.ForeColor = Color.White;
            hcBtn.Name = "hcBtn";
            hcBtn.UseVisualStyleBackColor = false;
            // 
            // dontresizeBox
            // 
            dontresizeBox.BackColor = Color.Transparent;
            resources.ApplyResources(dontresizeBox, "dontresizeBox");
            dontresizeBox.ForeColor = Color.White;
            dontresizeBox.Name = "dontresizeBox";
            dontresizeBox.UseVisualStyleBackColor = false;
            // 
            // SettingsForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImage = Properties.Resources.Settings_background;
            resources.ApplyResources(this, "$this");
            ControlBox = false;
            Controls.Add(dontresizeBox);
            Controls.Add(okBtn);
            Controls.Add(hcBtn);
            Controls.Add(resetBtn);
            Controls.Add(ramBar);
            Controls.Add(closeBtn);
            Controls.Add(faststartBox);
            Controls.Add(RAMLabel);
            Controls.Add(useProxy);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            Load += SettingsForm_Load;
            ((System.ComponentModel.ISupportInitialize)closeBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)okBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)ramBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)resetBtn).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label RAMLabel;
        private System.Windows.Forms.CheckBox useProxy;
        private CheckBox faststartBox;
        private PictureBox closeBtn;
        private PictureBox okBtn;
        private TrackBar ramBar;
        private PictureBox resetBtn;
        private CheckBox hcBtn;
        private CheckBox dontresizeBox;
    }
}