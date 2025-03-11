using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCRL
{
    public partial class SettingsForm : Form
    {



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
            Properties.Settings.Default.Save();
            ActiveForm.Close();
        }
    }
}
