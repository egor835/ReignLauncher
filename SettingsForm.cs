using System.Runtime.InteropServices;
using static RCRL.LauncherForm;

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
        public SettingsForm()
        {
            InitializeComponent();
            int sss = Screen.PrimaryScreen.Bounds.Height;
            plzresizeit(sss);
        }

        private async void SettingsForm_Load(object sender, EventArgs e)
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
            ramBar.Value = Int32.Parse(Properties.Settings.Default.RAM);
            RAMLabel.Text = "Выделенная память: " + ramBar.Value + " МБ";

        }

        private async void okBtn_release(object sender, EventArgs e)
        {
            Properties.Settings.Default.RAM = ramBar.Value.ToString();
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
            Properties.Settings.Default.Save();
            ActiveForm.Close();
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
            ActiveForm.Close();
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
            RAMLabel.Text = "Выделенная память: " + ramBar.Value + " МБ";
        }

        private void plzresizeit(int resol)
        {
            double k = 1;
            k = Convert.ToDouble(resol) / 1080F;
            if (k > 1.5F)
            { k = 1.5F; }

            this.MinimumSize = new Size(Convert.ToInt32(350 * k), Convert.ToInt32(400 * k));
            this.MaximumSize = new Size(Convert.ToInt32(350 * k), Convert.ToInt32(400 * k));
            this.ClientSize = new Size(Convert.ToInt32(350 * k), Convert.ToInt32(400 * k));


            useProxy.Font = new Font("Calibri", Convert.ToInt32(19 * k), FontStyle.Regular, GraphicsUnit.Pixel);
            faststartBox.Font = new Font("Calibri", Convert.ToInt32(19 * k), FontStyle.Regular, GraphicsUnit.Pixel);
            hcBtn.Font = new Font("Calibri", Convert.ToInt32(19 * k), FontStyle.Regular, GraphicsUnit.Pixel);
            RAMLabel.Font = new Font("Calibri", Convert.ToInt32(19 * k), FontStyle.Regular, GraphicsUnit.Pixel);
            RAMLabel.Location = new Point(Convert.ToInt32(12 * k), Convert.ToInt32(271 * k));

            resetBtn.Location = new Point(Convert.ToInt32(12 * k), Convert.ToInt32(348 * k));
            resetBtn.Size = new Size(Convert.ToInt32(150 * k), Convert.ToInt32(40 * k));

            okBtn.Location = new Point(Convert.ToInt32(185 * k), Convert.ToInt32(348 * k));
            okBtn.Size = new Size(Convert.ToInt32(150 * k), Convert.ToInt32(40 * k));

            closeBtn.Location = new Point(Convert.ToInt32(290 * k), Convert.ToInt32(0 * k));
            closeBtn.Size = new Size(Convert.ToInt32(60 * k), Convert.ToInt32(60 * k));



            useProxy.Location = new Point(Convert.ToInt32(12 * k), Convert.ToInt32(57 * k));
            useProxy.Size = new Size(Convert.ToInt32(326 * k), Convert.ToInt32(30 * k));

            faststartBox.Location = new Point(Convert.ToInt32(12 * k), Convert.ToInt32(93 * k));
            faststartBox.Size = new Size(Convert.ToInt32(326 * k), Convert.ToInt32(30 * k));

            hcBtn.Location = new Point(Convert.ToInt32(12 * k), Convert.ToInt32(129 * k));
            hcBtn.Size = new Size(Convert.ToInt32(326 * k), Convert.ToInt32(30 * k));

            ramBar.Location = new Point(Convert.ToInt32(12 * k), Convert.ToInt32(297 * k));
            ramBar.Size = new Size(Convert.ToInt32(326 * k), Convert.ToInt32(45 * k));

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, Convert.ToInt32(60 * k), Convert.ToInt32(60 * k)));
        }
    }
}
