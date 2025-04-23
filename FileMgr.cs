﻿using System.Collections.Generic;
using static RCRL.LauncherForm;

namespace RCRL
{
    internal class FileMgr
    {
        public async Task DownloadEveryFile(String whereis, String whereitneed, List<string> list, ProgressBar pb, Label tb)
        {
            foreach (var item in list.Select((value, index) => new { Value = value, Index = index }))
            {
                using (var client = new HttpClientDownloadWithProgress(Path.Combine(whereis, item.Value), Path.Combine(whereitneed, item.Value)))
                {
                    client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
                    {
                        pb.Value = Convert.ToInt32(((Convert.ToDouble(item.Index) / Convert.ToDouble(list.Count)) * 100) + (progressPercentage/ Convert.ToDouble(list.Count)));
                        tb.Text = $"[Загрузка {item.Value} ({item.Index}/{list.Count})]";
                    };
                    await client.StartDownload();
                }
            }
            tb.Text = "";
        }

        public async Task DownloadAndUnpack(String whereis, String whereitneed)
        {
            using (var client = new HttpClientDownloadWithProgress(whereis, Path.Combine(GlobalPaths.datapath, Path.GetFileName(whereis))))
            {
                await client.StartDownload();
            }
            System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(GlobalPaths.datapath, Path.GetFileName(whereis)), whereitneed, true);
            File.Delete(Path.Combine(GlobalPaths.datapath, Path.GetFileName(whereis)));
        }
        public async Task DownloadAndUnpack(String whereis, String whereitneed, ProgressBar pb, Label tb)
        {
            using (var client = new HttpClientDownloadWithProgress(whereis, Path.Combine(GlobalPaths.datapath, Path.GetFileName(whereis))))
            {
                client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
                {
                    pb.Value = Convert.ToInt32(progressPercentage);
                    tb.Text = $"[Загрузка {Path.GetFileName(whereis)}: {totalBytesDownloaded}/{totalFileSize}]";
                };
                await client.StartDownload();
            }
            tb.Text = "Распаковка файлов...";
            System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(GlobalPaths.datapath, Path.GetFileName(whereis)), whereitneed, true);
            File.Delete(Path.Combine(GlobalPaths.datapath, Path.GetFileName(whereis)));
            tb.Text = "";
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
}
