using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Installer.Forge;
using CmlLib.Core.Installers;
using CmlLib.Core.ProcessBuilder;
using Microsoft.VisualBasic.ApplicationServices;
using System.Net;
using System.Security.Policy;
using System.Text.Json;

namespace RCRL;

public partial class LauncherForm : Form
{
    public class ver
    {
        public string name { get; set; }
        public List<string> mods { get; set; }
    }
    public class rev
    {
        public List<ver> ver { get; set; }
    }
    public class Config
    {
        public string ip { get; set; }
        public string proxy { get; set; }
        public string port { get; set; }
        public string updateServer { get; set; }
    }

    public static class Globals
    {
        //path
        public static string mcpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".reigncraft");
        //config
        public static string jsonPath = Path.Combine(AppContext.BaseDirectory, "config.json");
        public static string json = File.ReadAllText(jsonPath);
        //versions
        public static string versionsPath = Path.Combine(mcpath, "versions.json");
        public static string versions = File.ReadAllText(versionsPath);
        public static string ModsVer;
    }

    private readonly MinecraftLauncher _launcher;
    public LauncherForm()
    {
        var mcpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".reigncraft");
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "config.json");
        var json = File.ReadAllText(jsonPath);
        Config? config = JsonSerializer.Deserialize<Config>(json);
        try { Directory.CreateDirectory(mcpath); }
        catch { }
        try
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(Path.Combine(config.updateServer, "versions.json"), Path.Combine(mcpath, "versions.json"));
            }
            _launcher = new MinecraftLauncher(new MinecraftPath(Globals.mcpath));
            InitializeComponent();
        }
        catch { MessageBox.Show("Проверьте своё интернет-соединение и повторите попытку.");
            Environment.Exit(0);
        }
    }

    private async void LauncherForm_Load(object sender, EventArgs e)
    {
        // Load previous used values in the inputs
        usernameInput.Text = Properties.Settings.Default.Username;
        cbVersion.Text = Properties.Settings.Default.Version;
        Globals.ModsVer = Properties.Settings.Default.ModsVer;
        if (string.IsNullOrEmpty(usernameInput.Text))
            usernameInput.Text = Environment.UserName;
        if (string.IsNullOrEmpty(cbVersion.Text))
            cbVersion.Text = "Full";
        if (string.IsNullOrEmpty(Globals.ModsVer))
            Globals.ModsVer = "0";
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

        //init config
        rev? revision = JsonSerializer.Deserialize<rev>(Globals.versions);


        // List all versions
        foreach (var version in revision.ver)
        { 
             cbVersion.Items.Add(version.name);
        }

    }

    private async void btnStart_Click(object sender, EventArgs e)
    {
        // Disable UI while launching
        this.Enabled = false;
        btnStart.Text = "Идёт загрузка...";
        var mcVersion = "1.20.1";
        
        //define fucking variables
        Config? config = JsonSerializer.Deserialize<Config>(Globals.json);
        rev? revision = JsonSerializer.Deserialize<rev>(Globals.versions);
        var servmodfolder = Path.Combine(Globals.mcpath, "servermods");
        var globmodfolder = Path.Combine(Globals.mcpath, "mods");
        var usermodfolder = Path.Combine(Globals.mcpath, "usermods");
        var port = config.port;
        var addr = config.ip;
        if (useProxy.Checked == true)
        {
            addr = config.proxy;
        }

        try
        {
           //install forge
            var byteProgress = new SyncProgress<ByteProgress>(_launcher_ProgressChanged);
            var fileProgress = new SyncProgress<InstallerProgressChangedEventArgs>(Launcher_FileChanged);
            var forge = new ForgeInstaller(_launcher);
            var version_name = await forge.Install(mcVersion, new ForgeInstallOptions
            {
                ByteProgress = byteProgress,
                FileProgress = fileProgress
            });
            
            
            //silly mod updater
            string readver;
            try
            {
                using (WebClient client = new WebClient())
                {
                    readver = client.DownloadString(Path.Combine(config.updateServer, "version"));
                }
                if (Int32.Parse(readver) > Int32.Parse(Globals.ModsVer))
                {
                    //mods
                    var downloadFileUrl = Path.Combine(config.updateServer, "mods.zip");
                    var destinationFilePath = Path.Combine(Globals.mcpath, "mods.zip");
                    using (var client = new HttpClientDownloadWithProgress(downloadFileUrl, destinationFilePath))
                    {
                        client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
                        {
                            pbFiles.Value = Convert.ToInt32(progressPercentage);
                            lbProgress.Text = $"[Updating mods: {totalBytesDownloaded}/{totalFileSize}]";
                        };

                        await client.StartDownload();
                    }
                    //README
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(Path.Combine(config.updateServer, "README.TXT"), Path.Combine(Globals.mcpath, "README.TXT"));
                    }
                    Globals.ModsVer = readver;
                    try
                    {
                        Array.ForEach(Directory.GetFiles(servmodfolder), File.Delete);
                    }
                    catch { }
                    System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(Globals.mcpath, "mods.zip"), servmodfolder);
                }
            }
            //if you somehow downloaded json, but fucked up on version
            catch (Exception ex)
            {
                lbProgress.Text = "Do not forget to enable proxy!";
            }


            //MODPACK CHANGER (plz hewp me)
            if (cbVersion.Text != Properties.Settings.Default.Version)
            {
                try {Array.ForEach(Directory.GetFiles(globmodfolder), File.Delete); }
                catch {Directory.CreateDirectory(globmodfolder); }
                
                ver? fullVersion = revision.ver.FirstOrDefault(v => v.name == cbVersion.Text);
                foreach (var mod in fullVersion.mods)
                {
                    File.Copy(Path.Combine(servmodfolder, mod), Path.Combine(globmodfolder, mod), true);
                }
                try 
                { 
                    foreach (var usermod in Directory.GetFiles(usermodfolder))
                    {
                        string modname = Path.GetFileName(usermod);
                        File.Copy(usermod, Path.Combine(globmodfolder, modname), true);
                    } 
                }
                catch { Directory.CreateDirectory(usermodfolder); }
            }

            //LAUNCH MINCERAFT and write vars to conf
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
            Properties.Settings.Default.ModsVer = Globals.ModsVer;
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




    //some random shit, idk, i stole this code
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
    public class HttpClientDownloadWithProgress : IDisposable
    {
        private readonly string _downloadUrl;
        private readonly string _destinationFilePath;

        private HttpClient _httpClient;

        public delegate void ProgressChangedHandler(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage);

        public event ProgressChangedHandler ProgressChanged;

        public HttpClientDownloadWithProgress(string downloadUrl, string destinationFilePath)
        {
            _downloadUrl = downloadUrl;
            _destinationFilePath = destinationFilePath;
        }

        public async Task StartDownload()
        {
            _httpClient = new HttpClient { Timeout = TimeSpan.FromDays(1) };

            using (var response = await _httpClient.GetAsync(_downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                await DownloadFileFromHttpResponseMessage(response);
        }

        private async Task DownloadFileFromHttpResponseMessage(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength;

            using (var contentStream = await response.Content.ReadAsStreamAsync())
                await ProcessContentStream(totalBytes, contentStream);
        }

        private async Task ProcessContentStream(long? totalDownloadSize, Stream contentStream)
        {
            var totalBytesRead = 0L;
            var readCount = 0L;
            var buffer = new byte[8192];
            var isMoreToRead = true;

            using (var fileStream = new FileStream(_destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
            {
                do
                {
                    var bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        isMoreToRead = false;
                        TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                        continue;
                    }

                    await fileStream.WriteAsync(buffer, 0, bytesRead);

                    totalBytesRead += bytesRead;
                    readCount += 1;

                    if (readCount % 100 == 0)
                        TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                }
                while (isMoreToRead);
            }
        }

        private void TriggerProgressChanged(long? totalDownloadSize, long totalBytesRead)
        {
            if (ProgressChanged == null)
                return;

            double? progressPercentage = null;
            if (totalDownloadSize.HasValue)
                progressPercentage = Math.Round((double)totalBytesRead / totalDownloadSize.Value * 100, 2);

            ProgressChanged(totalDownloadSize, totalBytesRead, progressPercentage);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
