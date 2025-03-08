using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Installer.Forge;
using CmlLib.Core.Installers;
using CmlLib.Core.ProcessBuilder;
using System;
using System.IO;
using System.Text.Json;

namespace RCRL;

public partial class LauncherForm : Form
{

    public class Config
    {
        public string ip { get; set; }
        public string proxy { get; set; }
        public string port { get; set; }
        public List<string> versions { get; set; }
    }

    

    private readonly MinecraftLauncher _launcher;
    public LauncherForm()
    {
        var mcpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".reign");
        _launcher = new MinecraftLauncher(new MinecraftPath(mcpath));

        InitializeComponent();

    }

    private async void LauncherForm_Load(object sender, EventArgs e)
    {
        // Load previous used values in the inputs
        usernameInput.Text = Properties.Settings.Default.Username;
        cbVersion.Text = Properties.Settings.Default.Version;
        // Set default username to Environment.UserName if empty
        if (string.IsNullOrEmpty(usernameInput.Text))
            usernameInput.Text = Environment.UserName;
        if (string.IsNullOrEmpty(cbVersion.Text))
            cbVersion.Text = "Default";
        
        if (string.IsNullOrEmpty(Properties.Settings.Default.Proxy) || Properties.Settings.Default.Proxy == "0")
        {
            useProxy.Checked = false;
        } 
        else 
        {
            useProxy.Checked = true;
        }

        ramBox.Minimum = 1024;
        ramBox.Maximum = 16384;

        if (string.IsNullOrEmpty(Properties.Settings.Default.RAM))
        {
            ramBox.Value = 4096;
        }
        else 
        { 
            ramBox.Value = Int32.Parse(Properties.Settings.Default.RAM); 
        }
            await listVersions();

    }

    private async Task listVersions(bool includeAll = false)
    {
        // Clear list
        cbVersion.Items.Clear();

        string jsonPath = Path.Combine(AppContext.BaseDirectory, "config.json");
        string json = File.ReadAllText(jsonPath);
        Config? config = JsonSerializer.Deserialize<Config>(json);

        // List all versions
        var versions = await _launcher.GetAllVersionsAsync();
        foreach (var version in config.versions)
        { 
             cbVersion.Items.Add(version);
        }

        // Default latest if not already set
        if (string.IsNullOrEmpty(cbVersion.Text))
            cbVersion.Text = "Full";
    }

    private async void btnStart_Click(object sender, EventArgs e)
    {
        // Disable UI while launching
        this.Enabled = false;
        btnStart.Text = "Идёт загрузка...";
        var mcVersion = "1.20.1";
        // Try to launch Minecraft with an Offline session
        string jsonPath = Path.Combine(AppContext.BaseDirectory, "config.json");
        string json = File.ReadAllText(jsonPath);
        Config? config = JsonSerializer.Deserialize<Config>(json);
        var port = config.port;
        var addr = config.ip;
        if (useProxy.Checked == true)
        {
            addr = config.proxy;
        }

        try
        {
            var byteProgress = new SyncProgress<ByteProgress>(_launcher_ProgressChanged);
            var fileProgress = new SyncProgress<InstallerProgressChangedEventArgs>(Launcher_FileChanged);
            var forge = new ForgeInstaller(_launcher);
            var version_name = await forge.Install(mcVersion, new ForgeInstallOptions
            {
                ByteProgress = byteProgress,
                FileProgress = fileProgress
            });


           
            var launchOption = new MLaunchOption
            {
                MaximumRamMb = Int32.Parse(ramBox.Text),
                Session = MSession.CreateOfflineSession(usernameInput.Text),
                ServerIp = addr,
                ServerPort = Int32.Parse(port),
            };


            Properties.Settings.Default.Username = usernameInput.Text;
            Properties.Settings.Default.Version = cbVersion.Text;
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



            var process = await _launcher.InstallAndBuildProcessAsync(version_name, launchOption);
            var processUtil = new ProcessWrapper(process);
            processUtil.StartWithEvents();
            await processUtil.WaitForExitTaskAsync();
        }
        catch (Exception ex)
        {
            // Show error
            MessageBox.Show(ex.ToString());
        }
        pbFiles.Value = 0;

        this.Enabled = true;
        btnStart.Text = "ЗАПУСК";
    }





    ByteProgress byteProgress;
    private void _launcher_ProgressChanged(ByteProgress e)
    {
        byteProgress = e;
    }

    InstallerProgressChangedEventArgs? fileProgress;
    private void Launcher_FileChanged(InstallerProgressChangedEventArgs e)
    {
        if (e.EventType == InstallerEventType.Done)
            fileProgress = e;
    }
    private void eventTimer_Tick(object sender, EventArgs e)
    {
        var bytePercentage = (int)(byteProgress.ProgressedBytes / (double)byteProgress.TotalBytes * 100);
        if (bytePercentage >= 0 && bytePercentage <= 100)
        {
            pbFiles.Value = bytePercentage;
            pbFiles.Maximum = 100;
        }
        if (fileProgress != null)
            lbProgress.Text = $"[{fileProgress.ProgressedTasks}/{fileProgress.TotalTasks}] {fileProgress.Name}";
    }
}
