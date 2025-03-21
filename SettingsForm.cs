using static RCRL.LauncherForm;

namespace RCRL
{
    public partial class SettingsForm : Form
    {

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
            closeBtn.Image = Properties.Resources.Exit_hover;
        }
        private void closeBtn_noHover(object sender, EventArgs e)
        {
            closeBtn.Image = Properties.Resources.Exit;
        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }
        private void okBtn_Hover(object sender, EventArgs e)
        {
            okBtn.Image = Properties.Resources.Accept_hover;
        }
        private void okBtn_noHover(object sender, EventArgs e)
        {
            okBtn.Image = Properties.Resources.Accept;
        }
        private void okBtn_press(object sender, EventArgs e)
        {
            okBtn.Image = Properties.Resources.Accept_clicked;
        }
        private void resetBtn_Hover(object sender, EventArgs e)
        {
            resetBtn.Image = Properties.Resources.Reset_hover;
        }
        private void resetBtn_noHover(object sender, EventArgs e)
        {
            resetBtn.Image = Properties.Resources.Reset;
        }
        private void resetBtn_press(object sender, EventArgs e)
        {
            resetBtn.Image = Properties.Resources.Reset_clicked;
        }
        private void ramBar_change(object sender, EventArgs e)
        {
            RAMLabel.Text = "Выделенная память: " + ramBar.Value + " МБ";
        }
    }
}
