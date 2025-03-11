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
            label1 = new Label();
            ramBox = new NumericUpDown();
            label3 = new Label();
            useProxy = new CheckBox();
            okBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)ramBox).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(149, 18);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 0;
            label1.Text = "насройки";
            // 
            // ramBox
            // 
            ramBox.Location = new Point(149, 123);
            ramBox.Name = "ramBox";
            ramBox.Size = new Size(142, 23);
            ramBox.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Minecraft Seven v2", 14.2499981F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(55, 122);
            label3.Name = "label3";
            label3.Size = new Size(78, 17);
            label3.TabIndex = 12;
            label3.Text = "Память:";
            // 
            // useProxy
            // 
            useProxy.BackColor = Color.Transparent;
            useProxy.CheckAlign = ContentAlignment.MiddleRight;
            useProxy.Font = new Font("Minecraft Seven v2", 14.2499981F, FontStyle.Regular, GraphicsUnit.Point);
            useProxy.ForeColor = Color.Black;
            useProxy.Location = new Point(32, 68);
            useProxy.Name = "useProxy";
            useProxy.Size = new Size(175, 42);
            useProxy.TabIndex = 13;
            useProxy.Text = "Использовать прокси:";
            useProxy.TextAlign = ContentAlignment.MiddleRight;
            useProxy.UseVisualStyleBackColor = false;
            // 
            // okBtn
            // 
            okBtn.Location = new Point(149, 187);
            okBtn.Name = "okBtn";
            okBtn.Size = new Size(94, 23);
            okBtn.TabIndex = 14;
            okBtn.Text = "применить";
            okBtn.UseVisualStyleBackColor = true;
            okBtn.Click += this.okBtn_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(403, 222);
            Controls.Add(okBtn);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(ramBox);
            Controls.Add(useProxy);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "SettingsForm";
            Text = "Form1";
            Load += SettingsForm_Load;
            ((System.ComponentModel.ISupportInitialize)ramBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown ramBox;
        private Label label3;
        private System.Windows.Forms.CheckBox useProxy;
        private Button okBtn;
    }
}