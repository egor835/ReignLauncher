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
            ramBox.Minimum = 1024;
            ramBox.Maximum = 16384;
            ramBox.Value = Int32.Parse(Properties.Settings.Default.RAM);

        }

        private async void okBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RAM = ramBox.Text;
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
            Properties.Settings.Default.Save();
            ActiveForm.Close();
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
    }
}
