using static RCRL.LauncherForm;

namespace RCRL
{
    internal class FileMgr
    {
        public async Task DownloadEveryFile(String whereis, String whereitneed, List<string> list, ProgressBar pb, Label tb)
        {
            foreach (var item in list.Select((value, index) => new { Value = value, Index = index }))
            {
                pb.Value = Convert.ToInt32((Convert.ToDouble(item.Index) / Convert.ToDouble(list.Count)) * 100);
                tb.Text = $"[Downloading {item.Value} ({item.Index}/{list.Count})]";
                DownloadFileSync(Path.Combine(whereis, item.Value), Path.Combine(whereitneed, item.Value));
            }
        }

        public async Task DownloadAndUnpack(String whereis, String whereitneed)
        {
            DownloadFileSync(whereis, Path.Combine(GlobalPaths.datapath, Path.GetFileName(whereis)));
            System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(GlobalPaths.datapath, Path.GetFileName(whereis)), whereitneed, true);
            File.Delete(Path.Combine(GlobalPaths.datapath, Path.GetFileName(whereis)));
        }

        public async Task cleanMcFolder(String mcpath)
        {
            try { Directory.Delete(Path.Combine(mcpath, "assets"), true); }
            catch (DirectoryNotFoundException ex) { }
            try { Directory.Delete(Path.Combine(mcpath, "libraries"), true); }
            catch (DirectoryNotFoundException ex) { }
            try { Directory.Delete(Path.Combine(mcpath, "runtime"), true); }
            catch (DirectoryNotFoundException ex) { }
            try { Directory.Delete(Path.Combine(mcpath, "versions"), true); }
            catch (DirectoryNotFoundException ex) { }
        }
        public async Task removeAllBut(String pathTo, IEnumerable<string> whatNeeded)
        {
            if (Directory.Exists(pathTo))
            {
                foreach (var element in Directory.GetFiles(pathTo))
                {
                    if (!whatNeeded.Contains(Path.GetFileName(element)))
                    {
                        try
                        {
                            File.Delete(element);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
        }

        public List<string> getListOfDownloads(String path, List<string> list)
        {
            Directory.CreateDirectory(path);
            List<string> todwn = new List<string>();
            foreach (var element in list)
            {
                if (!Directory.GetFiles(path).Contains(Path.Combine(path, element)))
                {
                    todwn.Add(element);
                }
            }
            return todwn;
        }
    }
}
