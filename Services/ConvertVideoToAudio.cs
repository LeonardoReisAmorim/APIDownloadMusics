using APIDownloadMP3.Models;
using APIDownloadMP3.Utils;
using DownloadMP3;

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
            try
            {
                foreach (var musicUrl in UrlsMusics.Split(","))
                {
                    var youtubeClient = new Video(musicUrl.Trim());
                    var video = await youtubeClient.GetVideoAsync();
                    var streamManifest = await youtubeClient.GetManifestAsync(video.Id);
                    var streamInfo = youtubeClient.GetAudioOnlyStreamsMP4(streamManifest);

                    string fileName = $"{video.Title}.{streamInfo.Container.Name}";
                    string pathVideoMP4 = Path.Combine(DirectoryUtils.FilesPath, fileName);

                    await youtubeClient.DownloadAsync(streamInfo, pathVideoMP4);

                    string pathVideoMP3 = Path.ChangeExtension(pathVideoMP4, ".mp3");

                    var convertVideo = new ConvertVideo(pathVideoMP4, pathVideoMP3);
                    convertVideo.ConvertMP4ToMP3();

                    FileUtils.DeleteFile(pathVideoMP4);
                }

                CreateZipAllMusics();
                var resultStream = GetStreamZipAsync();
                DirectoryUtils.DeleteDirectoryRoot();

                return resultStream;
            }
            catch (Exception ex)
            {
                DirectoryUtils.DeleteDirectoryRoot();
                throw;
            }
        }

        private void CreateZipAllMusics()
        {
            FileUtils.CreateZipAllMusics(DirectoryUtils.FilesPath, Path.Combine(DirectoryUtils.ZipPath, DirectoryUtils.ZipFileName));
        }

        private MemoryStream GetStreamZipAsync()
        {
            return FileUtils.GetStreamZipAsync(DirectoryUtils.ZipPath, DirectoryUtils.ZipFileName);
        }
    }
}