namespace RCRL
{
    partial class Gambling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gambling));
            anim = new Panel();
            eventTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();

            eventTimer.Tick += eventTimer_Tick;
            // 
            // anim
            // 
            anim.BackgroundImage = Properties.Resources.casino_001;
            anim.Location = new Point(0, 0);
            anim.Name = "anim";
            anim.Size = new Size(512, 512);
            anim.TabIndex = 0;
            // 
            // Gambling
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(512, 512);
            Controls.Add(anim);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Gambling";
            Text = "LET'S GO GAMBLING";
            ResumeLayout(false);
        }

        #endregion

        private Panel anim;
        private System.Windows.Forms.Timer eventTimer;
    }
}