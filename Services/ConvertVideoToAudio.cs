using APIDownloadMP3.Utils;
using DownloadMP3;
using System.IO.Compression;

namespace APIDownloadMP3.Services
{
    public class ConvertVideoToAudio
    {
        private DirectoryUtils DirectoryUtils { get; set; }
        private string UrlsMusics { get; set; }

        public ConvertVideoToAudio(string urlsMusics)
        {
            if (string.IsNullOrWhiteSpace(urlsMusics))
            {
                throw new ArgumentException();
            }

            DirectoryUtils = new DirectoryUtils();
            UrlsMusics = urlsMusics;
        }

        public async Task<Stream> ConverterVideoToAudio()
        {
            var resultStream = new MemoryStream();
            try
            {
                foreach (var musicUrl in UrlsMusics.Split(","))
                {
                    var youtube = new Video(musicUrl);
                    var video = await youtube.GetVideoAsync();

                    string fileName = string.Join("_", video.FullName.Split(Path.GetInvalidFileNameChars()));
                    string pathVideoMP4 = Path.Combine(DirectoryUtils.FilesPath, fileName);
                    string pathVideoMP3 = Path.ChangeExtension(pathVideoMP4, ".mp3");

                    await FileUtils.WriteAllBytesAsync(pathVideoMP4, video.GetBytes());

                    var convertVideo = new ConvertVideo(pathVideoMP4, pathVideoMP3);
                    convertVideo.ConvertMP4ToMP3();

                    FileUtils.DeleteFile(pathVideoMP4);
                }

                CreateZipAllMusics();
                resultStream = GetStreamZipAsync();
                DirectoryUtils.DeleteDirectoryRoot();
            }
            catch (Exception ex) 
            {
                throw;
            }

            return resultStream;
        }

        private void CreateZipAllMusics()
        {
            ZipFile.CreateFromDirectory(DirectoryUtils.FilesPath, Path.Combine(DirectoryUtils.ZipPath, DirectoryUtils.ZipFileName));
        }

        private MemoryStream GetStreamZipAsync()
        {
            var zipFilePath = Directory.GetFiles(DirectoryUtils.ZipPath, DirectoryUtils.ZipFileName, SearchOption.TopDirectoryOnly).First();
            var allBytesZip = File.ReadAllBytes(zipFilePath);
            return new MemoryStream(allBytesZip);
        }
    }
}
