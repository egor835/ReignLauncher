using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Installer.Forge;
using CmlLib.Core.Installers;
using CmlLib.Core.ProcessBuilder;
using CmlLib.Core.VersionLoader;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RCRL;

public partial class LauncherForm : Form
{
    //make window rounded
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

    //version getter
    public class ver
    {
        public string name { get; set; }
        public List<string> mods { get; set; }
        public List<string> shaders { get; set; }
        public List<string> resourcepacks { get; set; }
    }
    public class rev
    {
        public List<ver> ver { get; set; }
    }
    //newz
    public class newz
    {
        public string title { get; set; }
        public List<string> news { get; set; }
        public string king { get; set; }
    }
    //fetch config
    public class Config
    {
        public string ip { get; set; }
        public string proxy { get; set; }
        public string port { get; set; }
        public string updateServer { get; set; }
        public string versionName { get; set; }
    }

    public static class Globals
    {
        //path
        public static string mcpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".reigncraft");
        public static string datapath = Path.Combine(mcpath, "launcher_data");
        //config
        public static string jsonPath = Path.Combine(AppContext.BaseDirectory, "config.json");
        public static string json = File.ReadAllText(jsonPath);
        //versions
        public static string versionsPath = Path.Combine(datapath, "versions.json");
        public static string versions = File.ReadAllText(versionsPath);
        public static string ModsVer;
        //parameters
        public static bool isInternetHere = true;
        public static bool isLoading = false;
        public static bool notafirstrun = false;
        //form
        public static SettingsForm form = new SettingsForm();
    }

    //make window draggable
    private const int WM_NCHITTEST = 0x84;
    private const int HTCLIENT = 0x1;
    private const int HTCAPTION = 0x2;
    protected override void WndProc(ref Message message)
    {
        base.WndProc(ref message);

        if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
            message.Result = (IntPtr)HTCAPTION;
    }


    private readonly MinecraftLauncher _launcher;
    public LauncherForm()
    {
        Process[] pname = Process.GetProcessesByName("RCRL");
        if (pname.Length > 1)
        {
            MessageBox.Show("Лаунчер уже запущен. Закройте все предыдущие процессы и повторите попытку.");
            Environment.Exit(0);
        }

        //define vars
        var mcpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".reigncraft");
        var datapath = Path.Combine(mcpath, "launcher_data");
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "config.json");
        var json = File.ReadAllText(jsonPath);

        //fucking mess, which downloads versions.json from remote server and checks internet kenekshun
        Config? config = JsonSerializer.Deserialize<Config>(json);
        try { Directory.CreateDirectory(mcpath); }
        catch { }
        try { Directory.CreateDirectory(datapath); }
        catch { }
        try
        {
            String getFrom = Path.Combine(config.updateServer, "versions.json");
            String dwnTo = Path.Combine(datapath, "newversions.json");
            DownloadFileSync(getFrom, dwnTo);
            File.Move(Path.Combine(datapath, "newversions.json"), Path.Combine(datapath, "versions.json"), true);
        }
        catch (Exception ex)
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.MCVersion))
            {
                MessageBox.Show("Проверьте своё интернет-соединение перед первым запуском.");
                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show("Проверьте своё интернет-соединение.");
                Globals.isInternetHere = false;
            }
        }

        //and init this shit
        if (Globals.isInternetHere)
        {
            try
            {
                DownloadFileSync(Path.Combine(config.updateServer, "bg.png"), Path.Combine(Globals.datapath, "bg.png"));
                DownloadFileSync(Path.Combine(config.updateServer, "news.json"), Path.Combine(Globals.datapath, "news.json"));
                DownloadFileSync(Path.Combine(config.updateServer, "servers.dat"), Path.Combine(Globals.mcpath, "servers.dat"));
            }
            catch
            {
                MessageBox.Show("Судя по всему, лаунчер уже запущен. Закройте все предыдущие процессы и повторите попытку.");
                Environment.Exit(0);
            }
            _launcher = new MinecraftLauncher(new MinecraftPath(mcpath));
        }
        else
        {
            var path = new MinecraftPath(mcpath);
            var parameters = MinecraftLauncherParameters.CreateDefault(path);
            parameters.VersionLoader = new LocalJsonVersionLoader(path);
            _launcher = new MinecraftLauncher(parameters);
        }

        InitializeComponent();
        if (string.IsNullOrEmpty(Properties.Settings.Default.dontResizeIt))
        {
            Properties.Settings.Default.dontResizeIt = "0";
        }
        if (Properties.Settings.Default.dontResizeIt == "0")
        {
            plzresizeit();
        }
        else
        {
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 60, 60));
        }
    }

    private async void LauncherForm_Load(object sender, EventArgs e)
    {
        //define buttons and some дезигн shit
        Config? config = JsonSerializer.Deserialize<Config>(Globals.json);
        if (Globals.isInternetHere)
        {
            this.BackgroundImage = Image.FromFile(Path.Combine(Globals.datapath, "bg.png"));
        }
        hide("launcher_data");

        // Load previous used values in the inputs
        usernameInput.Text = Properties.Settings.Default.Username;
        Globals.ModsVer = Properties.Settings.Default.ModsVer;
        if (string.IsNullOrEmpty(Globals.ModsVer))
            Globals.ModsVer = "0";

        //first run checks
        if (string.IsNullOrEmpty(Properties.Settings.Default.Proxy))
        {
            Properties.Settings.Default.Proxy = "0";
        }
        if (string.IsNullOrEmpty(Properties.Settings.Default.RAM))
        {
            Properties.Settings.Default.RAM = "4096";
        }
        if (string.IsNullOrEmpty(Properties.Settings.Default.FastStart))
        {
            Properties.Settings.Default.FastStart = "0";
        }
        if (string.IsNullOrEmpty(Properties.Settings.Default.HighContrast))
        {
            Properties.Settings.Default.HighContrast = "0";
        }
        if (!Directory.Exists(Path.Combine(Globals.mcpath, "user_resourcepacks")))
        {
            Directory.CreateDirectory(Path.Combine(Globals.mcpath, "user_resourcepacks"));
        }
        if (!Directory.Exists(Path.Combine(Globals.mcpath, "user_shaderpacks")))
        {
            Directory.CreateDirectory(Path.Combine(Globals.mcpath, "user_shaderpacks"));
        }
        if (!Directory.Exists(Path.Combine(Globals.mcpath, "user_mods")))
        {
            Directory.CreateDirectory(Path.Combine(Globals.mcpath, "user_mods"));
        }
        await listVersions();

    }

    private async Task listVersions()
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

        if (!(cbVersion.Items.Contains(Properties.Settings.Default.Version)))
        {
            cbVersion.Text = cbVersion.Items[0].ToString();
        }
        else
        {
            cbVersion.Text = Properties.Settings.Default.Version;
        }

        //news
        if (Globals.isInternetHere)
        {
            string newsPath = Path.Combine(Globals.datapath, "news.json");
            string newsCont = File.ReadAllText(newsPath);
            newz? news = JsonSerializer.Deserialize<newz>(newsCont);
            NewsLabel.Text = news.title;
            NewsRTB.Text = "";
            foreach (var neww in news.news)
            {
                NewsRTB.Text += ("• " + neww + (Environment.NewLine + Environment.NewLine));
            }
        }
    }
    private void usernameInput_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnStart_release(this, e);
        }
    }
    private async void btnStart_release(object sender, EventArgs e)
    {
        //check username
        if (string.IsNullOrEmpty(usernameInput.Text))
        {
            MessageBox.Show("Введите никнейм");
        }
        else if (usernameInput.Text.Replace("_", "").Any(ch => !char.IsLetterOrDigit(ch)) || Regex.IsMatch(usernameInput.Text.Replace("_", ""), @"\p{IsCyrillic}"))
        {
            MessageBox.Show("Ваш никнейм не должен содержать:\n\n- пробелов\n- кириллицы\n- спецсимволов\n\nДопустимы только латинские буквы и цифры.");
        }
        else if (usernameInput.Text.Length > 16)
        {
            MessageBox.Show("Ваш никнейм слишком длинный!");
        }
        else
        {
            Globals.isLoading = true;
            // Disable UI while launchin
            stfu();
            btnStart.BackgroundImage = Properties.Resources.Play_install;
            var mcVersion = "1.20.1";

            //define fucking variables
            Config? config = JsonSerializer.Deserialize<Config>(Globals.json);
            rev? revision = JsonSerializer.Deserialize<rev>(Globals.versions);
            var servmodfolder = Path.Combine(Globals.mcpath, "servermods");
            var globmodfolder = Path.Combine(Globals.mcpath, "mods");
            var usermodfolder = Path.Combine(Globals.mcpath, "user_mods");
            var resourcepacks = Path.Combine(Globals.mcpath, "resourcepacks");
            var shaderpacks = Path.Combine(Globals.mcpath, "shaderpacks");
            var optionfile = Path.Combine(Globals.mcpath, "options.txt");
            var port = config.port;
            var addr = config.ip;
            if (!(Properties.Settings.Default.Proxy == "0"))
            {
                addr = config.proxy;
            }

            try
            {
                pbFiles.Visible = true;
                var version_name = "";
                if (Globals.isInternetHere)
                {
                    //install forge
                    eventTimer.Enabled = true;
                    var byteProgress = new SyncProgress<ByteProgress>(_launcher_ProgressChanged);
                    var fileProgress = new SyncProgress<InstallerProgressChangedEventArgs>(Launcher_FileChanged);
                    var forge = new ForgeInstaller(_launcher);
                    version_name = await forge.Install(mcVersion, config.versionName, new ForgeInstallOptions
                    {
                        ByteProgress = byteProgress,
                        FileProgress = fileProgress
                    });
                    eventTimer.Enabled = false;
                }
                else
                {
                    version_name = Properties.Settings.Default.MCVersion;
                }

                //silly mod updater
                if (Globals.isInternetHere)
                {
                    string readver;
                    try
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            readver = await client.GetStringAsync(Path.Combine(config.updateServer, "version"));
                        }
                        if (Int32.Parse(readver) > Int32.Parse(Globals.ModsVer))
                        {
                            //mods
                            using (var client = new HttpClientDownloadWithProgress(Path.Combine(config.updateServer, "mods.zip"), Path.Combine(Globals.mcpath, "mods.zip")))
                            {
                                client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
                                {
                                    pbFiles.Value = Convert.ToInt32(progressPercentage);
                                    lbProgress.Text = $"[Updating mods: {totalBytesDownloaded}/{totalFileSize}]";
                                };

                                await client.StartDownload();
                            }
                            using (var client = new HttpClientDownloadWithProgress(Path.Combine(config.updateServer, "resourcepacks.zip"), Path.Combine(Globals.mcpath, "resourcepacks.zip")))
                            {
                                client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
                                {
                                    pbFiles.Value = Convert.ToInt32(progressPercentage);
                                    lbProgress.Text = $"[Updating resourcepacks: {totalBytesDownloaded}/{totalFileSize}]";
                                };

                                await client.StartDownload();
                            }
                            using (var client = new HttpClientDownloadWithProgress(Path.Combine(config.updateServer, "shaders.zip"), Path.Combine(Globals.mcpath, "shaders.zip")))
                            {
                                client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
                                {
                                    pbFiles.Value = Convert.ToInt32(progressPercentage);
                                    lbProgress.Text = $"[Updating shaders: {totalBytesDownloaded}/{totalFileSize}]";
                                };

                                await client.StartDownload();
                            }
                            //README
                            DownloadFileSync(Path.Combine(config.updateServer, "README.TXT"), Path.Combine(Globals.mcpath, "README.TXT"));

                            Globals.ModsVer = readver;
                            lbProgress.Text = "Unpacking...";
                            try { Directory.Delete(servmodfolder, true); }
                            catch (Exception ex) { }
                            try { Directory.Delete(resourcepacks, true); }
                            catch (Exception ex) { }
                            try { Directory.Delete(shaderpacks, true); }
                            catch (Exception ex) { }
                            System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(Globals.mcpath, "mods.zip"), servmodfolder);
                            System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(Globals.mcpath, "resourcepacks.zip"), resourcepacks);
                            System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(Globals.mcpath, "shaders.zip"), shaderpacks);
                            File.Delete(Path.Combine(Globals.mcpath, "mods.zip"));
                            File.Delete(Path.Combine(Globals.mcpath, "resourcepacks.zip"));
                            File.Delete(Path.Combine(Globals.mcpath, "shaders.zip"));
                        }
                    }
                    //if you downloaded json, but somehow fucked up on version
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

                //add user's texturepak and shaders

                try
                {
                    foreach (var usertp in Directory.GetFiles(Path.Combine(Globals.mcpath, "user_resourcepacks")))
                    {
                        string tpname = Path.GetFileName(usertp);
                        File.Copy(usertp, Path.Combine(resourcepacks, tpname), true);
                    }
                }
                catch { Directory.CreateDirectory(Path.Combine(Globals.mcpath, "user_resourcepacks")); }

                try
                {
                    foreach (var usersp in Directory.GetFiles(Path.Combine(Globals.mcpath, "user_shaderpacks")))
                    {
                        string spname = Path.GetFileName(usersp);
                        File.Copy(usersp, Path.Combine(shaderpacks, spname), true);
                    }
                }
                catch { Directory.CreateDirectory(Path.Combine(Globals.mcpath, "user_shaderpacks")); }



                //MODPACK CHANGER (plz hewp me)
                try { Array.ForEach(Directory.GetFiles(globmodfolder), File.Delete); }
                catch { Directory.CreateDirectory(globmodfolder); }
                ver? fullVersion = revision.ver.FirstOrDefault(v => v.name == cbVersion.Text);
                lbProgress.Text = "Installing mods";
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

                //enable texturepacks
                var opline = "";
                if (File.Exists(optionfile))
                {
                    string[] arrLine = File.ReadAllLines(optionfile);
                    foreach (var item in arrLine.Select((value, index) => new { value, index }))
                    {
                        var lin = item.value;
                        var ind = item.index;
                        if (lin.Contains("resourcePacks"))
                        {
                            opline = "resourcePacks: [\"vanilla\",\"mod_resources\"";
                            if (Properties.Settings.Default.HighContrast == "1")
                            {
                                opline += ",\"high_contrast\"";
                            }
                            foreach (var rp in fullVersion.resourcepacks)
                            {
                                opline += ",\"file/" + rp + "\"";
                            }
                            foreach (var rp in Directory.GetFiles(Path.Combine(Globals.mcpath, "user_resourcepacks")))
                            {
                                string rpname = Path.GetFileName(rp);
                                opline += ",\"file/" + rpname + "\"";
                            }
                            opline += "]";
                            //MessageBox.Show(opline);
                            arrLine[ind] = opline;
                        }
                    }
                    File.WriteAllLines(optionfile, arrLine);
                }
                else
                {
                    opline = "resourcePacks: [\"vanilla\",\"mod_resources\"";
                    if (Properties.Settings.Default.HighContrast == "1")
                    {
                        opline += ",\"high_contrast\"";
                    }
                    foreach (var rp in fullVersion.resourcepacks)
                    {
                        opline += ",\"file/" + rp + "\"";
                    }
                    foreach (var rp in Directory.GetFiles(Path.Combine(Globals.mcpath, "user_resourcepacks")))
                    {
                        string rpname = Path.GetFileName(rp);
                        opline += ",\"file/" + rpname + "\"";
                    }
                    opline += "]\nlang:ru_ru";
                    File.WriteAllText(optionfile, opline);
                }

                //Hide the folders from stoopid users
                hide("assets");
                hide("libraries");
                hide("mods");
                hide("resourcepacks");
                hide("runtime");
                hide("shaderpacks");
                hide("versions");
                hide("servermods");
                hide("defaultconfigs");

                //LAUNCH MINCERAFT and write vars to conf
                pbFiles.Visible = false;
                lbProgress.Text = "Please wait...";
                var launchOption = new MLaunchOption
                {
                    MaximumRamMb = Int32.Parse(Properties.Settings.Default.RAM),
                    Session = MSession.CreateOfflineSession(usernameInput.Text),
                };
                if (Properties.Settings.Default.FastStart == "1")
                {
                    launchOption = new MLaunchOption
                    {
                        MaximumRamMb = Int32.Parse(Properties.Settings.Default.RAM),
                        Session = MSession.CreateOfflineSession(usernameInput.Text),
                        ServerIp = addr,
                        ServerPort = Int32.Parse(port),
                    };
                }
                Properties.Settings.Default.Username = usernameInput.Text;
                Properties.Settings.Default.Version = cbVersion.Text;
                Properties.Settings.Default.ModsVer = Globals.ModsVer;
                Properties.Settings.Default.MCVersion = version_name;
                Properties.Settings.Default.Save();
                var process = new Process();
                if (Globals.isInternetHere)
                {
                    process = await _launcher.InstallAndBuildProcessAsync(version_name, launchOption);
                }
                else
                {
                    process = await _launcher.BuildProcessAsync(version_name, launchOption);
                }
                var processUtil = new ProcessWrapper(process);
                processUtil.StartWithEvents();
                //await processUtil.WaitForExitTaskAsync();
                Environment.Exit(0);
                //stfu(false);
            }
            catch (Exception ex)
            {
                // Show error
                MessageBox.Show("Произошла ошибка.\nОтправьте репорт разрабочику.\n\n" + ex.ToString());
                Environment.Exit(0);
                //stfu(false);
            }
            pbFiles.Value = 0;
            //this.Enabled = true;
            Globals.isLoading = false;
            btnStart.BackgroundImage = Properties.Resources.Play;
        }
    }

    void DownloadFileSync(string url, string dest)
    {
        using var client = new HttpClient();
        var data = client.GetByteArrayAsync(url).GetAwaiter().GetResult();
        File.WriteAllBytes(dest, data);
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


    //disable form but not really
    private void stfu(bool Lock = true)
    {
        if (Lock == true)
        {
            closeBtn.Click -= closeBtn_Click;
            closeBtn.MouseHover -= closeBtn_Hover;
            btnStart.MouseUp -= btnStart_release;
            btnStart.MouseDown -= btnStart_press;
            btnStart.MouseEnter -= btnStart_Hover;
            settingsBtn.Click -= settingsBtn_Click;
            folderBtn.Click -= folderBtn_Click;
            settingsBtn.MouseEnter -= settingsBtn_Hover;
            folderBtn.MouseEnter -= folderBtn_Hover;
            usernameInput.ReadOnly = true;
            box1.BackgroundImage = Properties.Resources.box_disabled;
            var tempor = cbVersion.Text;
            cbVersion.Items.Clear();
            cbVersion.Items.Add(tempor);
            cbVersion.Text = tempor;
            try { Application.OpenForms["SettingsForm"].Close(); }
            catch { }
        }
        else
        {
            closeBtn.Click += closeBtn_Click;
            btnStart.MouseUp += btnStart_release;
            settingsBtn.Click += settingsBtn_Click;
            folderBtn.Click += folderBtn_Click;
            usernameInput.ReadOnly = false;
            //listVersions();
        }
    }

    private void hide(string foldername)
    {
        var folderpath = Path.Combine(Globals.mcpath, foldername);
        if (!Directory.Exists(folderpath))
        { Directory.CreateDirectory(folderpath); }
        DirectoryInfo di = new DirectoryInfo(folderpath);
        if (!di.Attributes.HasFlag(FileAttributes.Hidden))
        { di.Attributes |= FileAttributes.Hidden; }
    }

    //some random design code
    //closeBtn
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
        Environment.Exit(0);
    }
    //hideBtn
    private void hideBtn_Hover(object sender, EventArgs e)
    {
        hideBtn.BackgroundImage = Properties.Resources.hide_hover;
    }
    private void hideBtn_noHover(object sender, EventArgs e)
    {
        hideBtn.BackgroundImage = Properties.Resources.hide;
    }
    private void hideBtn_Click(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Minimized;
    }
    //startBtn
    private void btnStart_Hover(object sender, EventArgs e)
    {
        if (!Globals.isLoading)
        {
            btnStart.BackgroundImage = Properties.Resources.Play_hover;
        }
    }
    private void btnStart_noHover(object sender, EventArgs e)
    {
        if (!Globals.isLoading)
        {
            btnStart.BackgroundImage = Properties.Resources.Play;
        }
    }
    private void btnStart_press(object sender, EventArgs e)
    {
        btnStart.BackgroundImage = Properties.Resources.Play_clicked;
    }
    //settingsBtn
    private void settingsBtn_Hover(object sender, EventArgs e)
    {
        settingsBtn.BackgroundImage = Properties.Resources.Settings_hover;
    }
    private void settingsBtn_noHover(object sender, EventArgs e)
    {
        settingsBtn.BackgroundImage = Properties.Resources.Settings;
    }
    private void settingsBtn_Click(object sender, EventArgs e)
    {
        Globals.form.Show();
        Globals.form.BringToFront();
    }
    //folderBtn
    private void folderBtn_Hover(object sender, EventArgs e)
    {
        folderBtn.BackgroundImage = Properties.Resources.Mods_hover;
    }
    private void folderBtn_noHover(object sender, EventArgs e)
    {
        folderBtn.BackgroundImage = Properties.Resources.Mods;
    }
    private void folderBtn_Click(object sender, EventArgs e)
    {
        Process.Start("explorer.exe", Globals.mcpath);
    }
    //sunflower
    private void sunflower_Hover(object sender, EventArgs e)
    {
        sunflower.BackgroundImage = Properties.Resources.Sunflower_hover;
    }
    private void sunflower_noHover(object sender, EventArgs e)
    {
        sunflower.BackgroundImage = Properties.Resources.Sunflower;
    }
    private void sunflower_Click(object sender, EventArgs e)
    {
        Process.Start("explorer", "https://modrinth.com/organization/reign-devs");
    }
    //book
    private void book_Hover(object sender, EventArgs e)
    {
        book.BackgroundImage = Properties.Resources.Textbook_hover;
    }
    private void book_noHover(object sender, EventArgs e)
    {
        book.BackgroundImage = Properties.Resources.Textbook;
    }
    private void book_Click(object sender, EventArgs e)
    {
        Process.Start("explorer", "https://t.me/reignmod");
    }
    private void cbVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((cbVersion.SelectedIndex == (cbVersion.Items.Count - 1)) && (Globals.isLoading == false))
        {
            if (Globals.notafirstrun == true)
            {
                DialogResult PROCEED = MessageBox.Show("Вы выбрали пользовательские моды в качестве модпака.\nУчтите, что в этом режиме сборки от создателей ReignCraft не будут использоваться, вы должны добавить свой модпак в usermods\nВы хотите открыть папку с пользовательскими модами сейчас?", "Значит ты выбрал Usermods...", MessageBoxButtons.OKCancel);
                if (PROCEED == DialogResult.OK)
                {
                    Process.Start("explorer.exe", Path.Combine(Globals.mcpath, "user_mods"));
                }
            }
        }
        Globals.notafirstrun = true;
    }
    private void usernameInput_TextChanged(object sender, EventArgs e)
    {
        switch (usernameInput.Text)
        {
            case "ilaa70":
                easterLabel.Text = "/ban";
                break;
            case "PetrCHess":
                easterLabel.Text = "наведи суету";
                break;
            case "cnuuyy":
                easterLabel.Text = "подписывайтесь на notNTඞ";
                break;
            case "LEGIMENCY":
                easterLabel.Text = "Идём собирать хворост";
                break;
            case "_machini_":
                easterLabel.Text = "Первый король онлайн";
                break;
            case "Noh4don":
                easterLabel.Text = "что с братом?";
                break;
            case "T4lziar":
                easterLabel.Text = "Свиное рагу";
                break;
            case "JustGera":
                easterLabel.Text = "Я люблю Солнечный оплот ❤️";
                break;
            case "EvisTS":
                easterLabel.Text = "Опять строить...";
                break;
            case "Desmit":
                easterLabel.Text = "Первый Лук Мезенхольма";
                break;
            case "Arminiy777":
                easterLabel.Text = "гномыгномыгномыгномыгномыгномыгномыгномыгномыгномыгномы";
                break;
            case "Shinkiro":
                easterLabel.Text = "сожги их всех";
                break;
            default:
                easterLabel.Text = "";
                break;
        }
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
            k = (Convert.ToDouble(width) / 1920F);
            if (k > 1.5F) { k = 1.5F; }
        }

        cbVersion.Font = new Font("Calibri", Convert.ToInt32(cbVersion.Font.Size * k), FontStyle.Regular, GraphicsUnit.Pixel);
        lbProgress.Font = new Font("Calibri", Convert.ToInt32(lbProgress.Font.Size * k), FontStyle.Regular, GraphicsUnit.Pixel);
        usernameInput.Font = new Font("Calibri", Convert.ToInt32(usernameInput.Font.Size * k), FontStyle.Regular, GraphicsUnit.Pixel);
        NewsLabel.Font = new Font("Calibri", Convert.ToInt32(NewsLabel.Font.Size * k), FontStyle.Regular, GraphicsUnit.Pixel);
        NewsRTB.Font = new Font("Calibri", Convert.ToInt32(NewsRTB.Font.Size * k), FontStyle.Regular, GraphicsUnit.Pixel);
        easterLabel.Font = new Font("Calibri", Convert.ToInt32(easterLabel.Font.Size * k), FontStyle.Regular, GraphicsUnit.Pixel);

        cbVersion.Location = new Point(Convert.ToInt32(cbVersion.Location.X * k), Convert.ToInt32(cbVersion.Location.Y * k));
        cbVersion.Size = new Size(Convert.ToInt32(cbVersion.ClientSize.Width * k), Convert.ToInt32(cbVersion.ClientSize.Height * k));

        pbFiles.Location = new Point(Convert.ToInt32(pbFiles.Location.X * k), Convert.ToInt32(pbFiles.Location.Y * k));
        pbFiles.Size = new Size(Convert.ToInt32(pbFiles.ClientSize.Width * k), Convert.ToInt32(pbFiles.ClientSize.Height * k));

        lbProgress.Location = new Point(Convert.ToInt32(lbProgress.Location.X * k), Convert.ToInt32(lbProgress.Location.Y * k));
        lbProgress.Size = new Size(Convert.ToInt32(lbProgress.ClientSize.Width * k), Convert.ToInt32(lbProgress.ClientSize.Height * k));

        btnStart.Location = new Point(Convert.ToInt32(btnStart.Location.X * k), Convert.ToInt32(btnStart.Location.Y * k));
        btnStart.Size = new Size(Convert.ToInt32(btnStart.ClientSize.Width * k), Convert.ToInt32(btnStart.ClientSize.Height * k));

        usernameInput.Location = new Point(Convert.ToInt32(usernameInput.Location.X * k), Convert.ToInt32(usernameInput.Location.Y * k));
        usernameInput.Size = new Size(Convert.ToInt32(usernameInput.ClientSize.Width * k), Convert.ToInt32(usernameInput.ClientSize.Height * k));

        settingsBtn.Location = new Point(Convert.ToInt32(settingsBtn.Location.X * k), Convert.ToInt32(settingsBtn.Location.Y * k));
        settingsBtn.Size = new Size(Convert.ToInt32(settingsBtn.ClientSize.Width * k), Convert.ToInt32(settingsBtn.ClientSize.Height * k));

        folderBtn.Location = new Point(Convert.ToInt32(folderBtn.Location.X * k), Convert.ToInt32(folderBtn.Location.Y * k));
        folderBtn.Size = new Size(Convert.ToInt32(folderBtn.ClientSize.Width * k), Convert.ToInt32(folderBtn.ClientSize.Height * k));

        NewsLabel.Location = new Point(Convert.ToInt32(NewsLabel.Location.X * k), Convert.ToInt32(NewsLabel.Location.Y * k));
        NewsLabel.Size = new Size(Convert.ToInt32(NewsLabel.ClientSize.Width * k), Convert.ToInt32(NewsLabel.ClientSize.Height * k));

        logo.Location = new Point(Convert.ToInt32(logo.Location.X * k), Convert.ToInt32(logo.Location.Y * k));
        logo.Size = new Size(Convert.ToInt32(logo.ClientSize.Width * k), Convert.ToInt32(logo.ClientSize.Height * k));

        crown.Location = new Point(Convert.ToInt32(crown.Location.X * k), Convert.ToInt32(crown.Location.Y * k));
        crown.Size = new Size(Convert.ToInt32(crown.ClientSize.Width * k), Convert.ToInt32(crown.ClientSize.Height * k));

        closeBtn.Location = new Point(Convert.ToInt32(closeBtn.Location.X * k), Convert.ToInt32(closeBtn.Location.Y * k));
        closeBtn.Size = new Size(Convert.ToInt32(closeBtn.ClientSize.Width * k), Convert.ToInt32(closeBtn.ClientSize.Height * k));

        nickIMG.Location = new Point(Convert.ToInt32(nickIMG.Location.X * k), Convert.ToInt32(nickIMG.Location.Y * k));
        nickIMG.Size = new Size(Convert.ToInt32(nickIMG.ClientSize.Width * k), Convert.ToInt32(nickIMG.ClientSize.Height * k));

        buildsIMG.Location = new Point(Convert.ToInt32(buildsIMG.Location.X * k), Convert.ToInt32(buildsIMG.Location.Y * k));
        buildsIMG.Size = new Size(Convert.ToInt32(buildsIMG.ClientSize.Width * k), Convert.ToInt32(buildsIMG.ClientSize.Height * k));

        box1.Location = new Point(Convert.ToInt32(box1.Location.X * k), Convert.ToInt32(box1.Location.Y * k));
        box1.Size = new Size(Convert.ToInt32(box1.ClientSize.Width * k), Convert.ToInt32(box1.ClientSize.Height * k));

        box2.Location = new Point(Convert.ToInt32(box2.Location.X * k), Convert.ToInt32(box2.Location.Y * k));
        box2.Size = new Size(Convert.ToInt32(box2.ClientSize.Width * k), Convert.ToInt32(box2.ClientSize.Height * k));

        NewsRTB.Location = new Point(Convert.ToInt32(NewsRTB.Location.X * k), Convert.ToInt32(NewsRTB.Location.Y * k));
        NewsRTB.Size = new Size(Convert.ToInt32(NewsRTB.ClientSize.Width * k), Convert.ToInt32(NewsRTB.ClientSize.Height * k));

        sunflower.Location = new Point(Convert.ToInt32(sunflower.Location.X * k), Convert.ToInt32(sunflower.Location.Y * k));
        sunflower.Size = new Size(Convert.ToInt32(sunflower.ClientSize.Width * k), Convert.ToInt32(sunflower.ClientSize.Height * k));

        book.Location = new Point(Convert.ToInt32(book.Location.X * k), Convert.ToInt32(book.Location.Y * k));
        book.Size = new Size(Convert.ToInt32(book.ClientSize.Width * k), Convert.ToInt32(book.ClientSize.Height * k));

        easterLabel.Location = new Point(Convert.ToInt32(easterLabel.Location.X * k), Convert.ToInt32(easterLabel.Location.Y * k));
        easterLabel.Size = new Size(Convert.ToInt32(easterLabel.ClientSize.Width * k), Convert.ToInt32(easterLabel.ClientSize.Height * k));

        hideBtn.Location = new Point(Convert.ToInt32(hideBtn.Location.X * k), Convert.ToInt32(hideBtn.Location.Y * k));
        hideBtn.Size = new Size(Convert.ToInt32(hideBtn.ClientSize.Width * k), Convert.ToInt32(hideBtn.ClientSize.Height * k));

        this.MinimumSize = new Size(Convert.ToInt32(1200 * k), Convert.ToInt32(800 * k));
        this.MaximumSize = new Size(Convert.ToInt32(1200 * k), Convert.ToInt32(800 * k));
        this.ClientSize = new Size(Convert.ToInt32(1200 * k), Convert.ToInt32(800 * k));

        Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, Convert.ToInt32(60 * k), Convert.ToInt32(60 * k)));
    }
}
