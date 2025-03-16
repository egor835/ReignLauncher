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
            SettingsLabel = new Label();
            ramBox = new NumericUpDown();
            RAMLabel = new Label();
            useProxy = new CheckBox();
            okBtn = new Button();
            faststartBox = new CheckBox();
            closeBtn = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)ramBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)closeBtn).BeginInit();
            SuspendLayout();
            // 
            // SettingsLabel
            // 
            SettingsLabel.AutoSize = true;
            SettingsLabel.BackColor = Color.Transparent;
            SettingsLabel.Font = new Font("Calibri", 29F, FontStyle.Regular, GraphicsUnit.Pixel);
            SettingsLabel.ForeColor = Color.Black;
            SettingsLabel.Location = new Point(174, 28);
            SettingsLabel.Name = "SettingsLabel";
            SettingsLabel.Size = new Size(145, 36);
            SettingsLabel.TabIndex = 0;
            SettingsLabel.Text = "Настройки";
            // 
            // ramBox
            // 
            ramBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            ramBox.Location = new Point(12, 265);
            ramBox.Name = "ramBox";
            ramBox.Size = new Size(176, 23);
            ramBox.TabIndex = 11;
            // 
            // RAMLabel
            // 
            RAMLabel.AutoSize = true;
            RAMLabel.BackColor = Color.Transparent;
            RAMLabel.Font = new Font("Calibri", 19F, FontStyle.Regular, GraphicsUnit.Pixel);
            RAMLabel.ForeColor = Color.Black;
            RAMLabel.Location = new Point(12, 239);
            RAMLabel.Name = "RAMLabel";
            RAMLabel.Size = new Size(176, 23);
            RAMLabel.TabIndex = 12;
            RAMLabel.Text = "Выделенная память:";
            RAMLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // useProxy
            // 
            useProxy.BackColor = Color.Transparent;
            useProxy.CheckAlign = ContentAlignment.MiddleRight;
            useProxy.Font = new Font("Calibri", 19F, FontStyle.Regular, GraphicsUnit.Pixel);
            useProxy.ForeColor = Color.Black;
            useProxy.Location = new Point(281, 119);
            useProxy.Name = "useProxy";
            useProxy.Size = new Size(169, 57);
            useProxy.TabIndex = 13;
            useProxy.Text = "Использовать прокси:";
            useProxy.TextAlign = ContentAlignment.MiddleRight;
            useProxy.UseVisualStyleBackColor = false;
            // 
            // okBtn
            // 
            okBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            okBtn.Location = new Point(394, 265);
            okBtn.Name = "okBtn";
            okBtn.RightToLeft = RightToLeft.No;
            okBtn.Size = new Size(94, 23);
            okBtn.TabIndex = 14;
            okBtn.Text = "Применить";
            okBtn.UseVisualStyleBackColor = true;
            okBtn.Click += okBtn_Click;
            // 
            // faststartBox
            // 
            faststartBox.BackColor = Color.Transparent;
            faststartBox.CheckAlign = ContentAlignment.MiddleRight;
            faststartBox.Font = new Font("Calibri", 19F, FontStyle.Regular, GraphicsUnit.Pixel);
            faststartBox.ForeColor = Color.Black;
            faststartBox.Location = new Point(27, 119);
            faststartBox.Name = "faststartBox";
            faststartBox.Size = new Size(211, 57);
            faststartBox.TabIndex = 15;
            faststartBox.Text = "Автоматически заходить на сервер";
            faststartBox.TextAlign = ContentAlignment.MiddleRight;
            faststartBox.UseVisualStyleBackColor = false;
            // 
            // closeBtn
            // 
            closeBtn.BackColor = Color.Transparent;
            closeBtn.ForeColor = Color.Transparent;
            closeBtn.Image = Properties.Resources.Exit_black;
            closeBtn.Location = new Point(471, 9);
            closeBtn.Margin = new Padding(0);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(20, 20);
            closeBtn.TabIndex = 21;
            closeBtn.TabStop = false;
            closeBtn.Click += closeBtn_Click;
            closeBtn.MouseEnter += closeBtn_Hover;
            closeBtn.MouseLeave += closeBtn_noHover;
            // 
            // SettingsForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(500, 300);
            ControlBox = false;
            Controls.Add(closeBtn);
            Controls.Add(faststartBox);
            Controls.Add(okBtn);
            Controls.Add(SettingsLabel);
            Controls.Add(RAMLabel);
            Controls.Add(ramBox);
            Controls.Add(useProxy);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(500, 300);
            MinimizeBox = false;
            MinimumSize = new Size(500, 300);
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Настройки лаунчера";
            Load += SettingsForm_Load;
            ((System.ComponentModel.ISupportInitialize)ramBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)closeBtn).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label SettingsLabel;
        private NumericUpDown ramBox;
        private Label RAMLabel;
        private System.Windows.Forms.CheckBox useProxy;
        private Button okBtn;
        private CheckBox faststartBox;
        private PictureBox closeBtn;
    }
}