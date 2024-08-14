using DownloadMP3;

namespace APIDownloadMP3.Services
{
    public static class ConvertVideoToAudio
    {
        public static async Task<int> ConverterVideoToAudio(string urlsMusics)
        {
            var source = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            try
            {
                foreach (var musicUrl in urlsMusics.Split(","))
                {
                    var youtube = new Video(musicUrl);
                    var video = await youtube.GetVideoAsync();
                    string pathVideoMP4 = Path.Join(source, "\\", video.FullName);
                    string pathVideoMP3 = $"{pathVideoMP4.Replace(".mp4", "")}.mp3";
                    await FileUtils.WriteAllBytesAsync(pathVideoMP4, video.GetBytes());

                    var convertVideo = new ConvertVideo(pathVideoMP4, pathVideoMP3);
                    convertVideo.ConvertMP4ToMP3();

                    FileUtils.DeleteFile(pathVideoMP4);
                }
            }
            catch (Exception ex) 
            {
                throw;
            }

            return urlsMusics.Split(",").Length;
        }
    }
}
