using System.Runtime.InteropServices;
using static RCRL.LauncherForm;
using Microsoft.VisualBasic;

namespace RCRL
{

    public partial class SettingsForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }
        static ulong GetTotalMemory()
        {
            return new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory/1024L/1024L;
        }

        public SettingsForm()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(Properties.Settings.Default.RAM))
            {
                Properties.Settings.Default.RAM = "4096";
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.dontResizeIt))
            {
                Properties.Settings.Default.dontResizeIt = "0";
            }
            if (Properties.Settings.Default.dontResizeIt == "0") {
                plzresizeit();
            } else {
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 60, 60));
            }
        }

        private async void SettingsForm_Load(object sender, EventArgs e)
        {
            initplz();
        }

        private void initplz()
        {
            if (Properties.Settings.Default.Proxy == "0")
            {
                useProxy.Checked = false;
            }
            else
            {
                useProxy.Checked = true;
            }
            if (Properties.Settings.Default.FastStart == "0")
            {
                faststartBox.Checked = false;
            }
            else
            {
                faststartBox.Checked = true;
            }
            if (Properties.Settings.Default.HighContrast == "0")
            {
                hcBtn.Checked = false;
            }
            else
            {
                hcBtn.Checked = true;
            }
            if (Properties.Settings.Default.dontResizeIt == "0")
            {
                dontresizeBox.Checked = false;
            }
            else
            {
                dontresizeBox.Checked = true;
            }
            int maxram = Convert.ToInt32(GetTotalMemory())-2047;
            if (maxram < 1024) { maxram = 16384; }
            ramBar.Maximum = (maxram-1024)/512;
            if (Int32.Parse(Properties.Settings.Default.RAM) > maxram) { ramToBar(maxram); }
            else { ramToBar(Int32.Parse(Properties.Settings.Default.RAM)); }
            RAMLabel.Text = "Выделенная память: " + barToRam() + " МБ";
        }
        private async void okBtn_release(object sender, EventArgs e)
        {
            Properties.Settings.Default.RAM = barToRam().ToString();
            if (useProxy.Checked == false)
            {
                Properties.Settings.Default.Proxy = "0";
            }
            else
            {
                Properties.Settings.Default.Proxy = "1";
            }
            if (faststartBox.Checked == false)
            {
                Properties.Settings.Default.FastStart = "0";
            }
            else
            {
                Properties.Settings.Default.FastStart = "1";
            }
            if (hcBtn.Checked == false)
            {
                Properties.Settings.Default.HighContrast = "0";
            }
            else
            {
                Properties.Settings.Default.HighContrast = "1";
            }
            if (dontresizeBox.Checked == false)
            {
                Properties.Settings.Default.dontResizeIt = "0";
            }
            else
            {
                Properties.Settings.Default.dontResizeIt = "1";
            }
            Properties.Settings.Default.Save();
            ActiveForm.Hide();
        }
        private async void resetBtn_release(object sender, EventArgs e)
        {
            DialogResult PROCEED = MessageBox.Show("Вы уверены, что хотите удалить все данные лаунчера, включая все моды, в том числе и пользовательские?", "Покончить с этим", MessageBoxButtons.OKCancel);
            if (PROCEED == DialogResult.OK)
            {
                string mcpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".reigncraft");
                try { Directory.Delete(mcpath, true); }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();
                Environment.Exit(0);
            }
        }

        private void closeBtn_Hover(object sender, EventArgs e)
        {
            closeBtn.BackgroundImage = Properties.Resources.Exit_hover;
        }
        private void closeBtn_noHover(object sender, EventArgs e)
        {
            closeBtn.BackgroundImage = Properties.Resources.Exit;
        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            initplz();
            ActiveForm.Hide();
        }
        private void okBtn_Hover(object sender, EventArgs e)
        {
            okBtn.BackgroundImage = Properties.Resources.Accept_hover;
        }
        private void okBtn_noHover(object sender, EventArgs e)
        {
            okBtn.BackgroundImage = Properties.Resources.Accept;
        }
        private void okBtn_press(object sender, EventArgs e)
        {
            okBtn.BackgroundImage = Properties.Resources.Accept_clicked;
        }
        private void resetBtn_Hover(object sender, EventArgs e)
        {
            resetBtn.BackgroundImage = Properties.Resources.Reset_hover;
        }
        private void resetBtn_noHover(object sender, EventArgs e)
        {
            resetBtn.BackgroundImage = Properties.Resources.Reset;
        }
        private void resetBtn_press(object sender, EventArgs e)
        {
            resetBtn.BackgroundImage = Properties.Resources.Reset_clicked;
        }
        private void ramBar_change(object sender, EventArgs e)
        {
            RAMLabel.Text = "Выделенная память: " + barToRam() + " МБ";
        }

        private int barToRam()
        {
            return (ramBar.Value*512)+1024;
        }

        private void ramToBar(int ramm)
        {
            
            ramBar.Value = ((ramm-1024) / 512);
        }

        private void plzresizeit()
        {
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            double k = 1;
            if (width > height)
            {
                k = (Convert.ToDouble(height) / 1080F) * 0.8;
                if (k > 1.5F) { k = 1.5F; }
            }
            else
            {
                k = Convert.ToDouble(width) / 1920F;
                if (k > 1.5F) { k = 1.5F; }
            }

            useProxy.Font = new Font("Calibri", Convert.ToInt32(useProxy.Font.Size * k), FontStyle.Regular, GraphicsUnit.Pixel);
            faststartBox.Font = new Font("Calibri", Convert.ToInt32(faststartBox.Font.Size * k), FontStyle.Regular, GraphicsUnit.Pixel);
            hcBtn.Font = new Font("Calibri", Convert.ToInt32(hcBtn.Font.Size * k), FontStyle.Regular, GraphicsUnit.Pixel);
            dontresizeBox.Font = new Font("Calibri", Convert.ToInt32(dontresizeBox.Font.Size * k), FontStyle.Regular, GraphicsUnit.Pixel);
            RAMLabel.Font = new Font("Calibri", Convert.ToInt32(RAMLabel.Font.Size * k), FontStyle.Regular, GraphicsUnit.Pixel);

            RAMLabel.Location = new Point(Convert.ToInt32(RAMLabel.Location.X * k), Convert.ToInt32(RAMLabel.Location.Y * k));

            resetBtn.Location = new Point(Convert.ToInt32(resetBtn.Location.X * k), Convert.ToInt32(resetBtn.Location.Y * k));
            resetBtn.Size = new Size(Convert.ToInt32(resetBtn.ClientSize.Width * k), Convert.ToInt32(resetBtn.ClientSize.Height * k));

            okBtn.Location = new Point(Convert.ToInt32(okBtn.Location.X * k), Convert.ToInt32(okBtn.Location.Y * k));
            okBtn.Size = new Size(Convert.ToInt32(okBtn.ClientSize.Width * k), Convert.ToInt32(okBtn.ClientSize.Height * k));

            closeBtn.Location = new Point(Convert.ToInt32(closeBtn.Location.X * k), Convert.ToInt32(closeBtn.Location.Y * k));
            closeBtn.Size = new Size(Convert.ToInt32(closeBtn.ClientSize.Width * k), Convert.ToInt32(closeBtn.ClientSize.Height * k));

            dontresizeBox.Location = new Point(Convert.ToInt32(dontresizeBox.Location.X * k), Convert.ToInt32(dontresizeBox.Location.Y * k));
            dontresizeBox.Size = new Size(Convert.ToInt32(dontresizeBox.ClientSize.Width * k), Convert.ToInt32(dontresizeBox.ClientSize.Height * k));

            useProxy.Location = new Point(Convert.ToInt32(useProxy.Location.X * k), Convert.ToInt32(useProxy.Location.Y * k));
            useProxy.Size = new Size(Convert.ToInt32(useProxy.ClientSize.Width * k), Convert.ToInt32(useProxy.ClientSize.Height * k));

            faststartBox.Location = new Point(Convert.ToInt32(faststartBox.Location.X * k), Convert.ToInt32(faststartBox.Location.Y * k));
            faststartBox.Size = new Size(Convert.ToInt32(faststartBox.ClientSize.Width * k), Convert.ToInt32(faststartBox.ClientSize.Height * k));

            hcBtn.Location = new Point(Convert.ToInt32(hcBtn.Location.X * k), Convert.ToInt32(hcBtn.Location.Y * k));
            hcBtn.Size = new Size(Convert.ToInt32(hcBtn.ClientSize.Width * k), Convert.ToInt32(hcBtn.ClientSize.Height * k));

            ramBar.Location = new Point(Convert.ToInt32(ramBar.Location.X * k), Convert.ToInt32(ramBar.Location.Y * k));
            ramBar.Size = new Size(Convert.ToInt32(ramBar.ClientSize.Width * k), Convert.ToInt32(ramBar.ClientSize.Height * k));

            this.MinimumSize = new Size(Convert.ToInt32(350 * k), Convert.ToInt32(400 * k));
            this.MaximumSize = new Size(Convert.ToInt32(350 * k), Convert.ToInt32(400 * k));
            this.ClientSize = new Size(Convert.ToInt32(350 * k), Convert.ToInt32(400 * k));

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, Convert.ToInt32(60 * k), Convert.ToInt32(60 * k)));
        }
    }
}
