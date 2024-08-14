using VideoLibrary;

namespace DownloadMP3
{
    public class Video
    {
        public string UrlVideo { get; set; }
        private YouTube YouTube { get; set; }

        public Video(string urlvideo)
        {
            if (String.IsNullOrWhiteSpace(urlvideo))
            {
                throw new ArgumentNullException();
            }

            UrlVideo = urlvideo;
            YouTube = YouTube.Default;
        }

        public async Task<YouTubeVideo> GetVideoAsync()
        {
            return await YouTube.GetVideoAsync(UrlVideo);
        }
    }
}
