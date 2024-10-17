using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace APIDownloadMP3.Models
{
    public class Video
    {
        public string UrlVideo { get; set; }
        private YoutubeClient YouTubeClient { get; set; }

        public Video(string urlvideo)
        {
            if (string.IsNullOrWhiteSpace(urlvideo))
            {
                throw new ArgumentNullException();
            }

            UrlVideo = urlvideo;
            YouTubeClient = new YoutubeClient();
        }

        public async Task<YoutubeExplode.Videos.Video> GetVideoAsync()
        {
            return await YouTubeClient.Videos.GetAsync(UrlVideo);
        }

        public async Task<StreamManifest> GetManifestAsync(YoutubeExplode.Videos.VideoId videoId)
        {
           return await YouTubeClient.Videos.Streams.GetManifestAsync(videoId);
        }

        public AudioOnlyStreamInfo GetAudioOnlyStreamsMP4(StreamManifest streamManifest)
        {
            return streamManifest.GetAudioOnlyStreams()?.FirstOrDefault(x => x.Container == Container.Mp4);
        }

        public async Task DownloadAsync(AudioOnlyStreamInfo audioOnlyStreamInfo, string source)
        {
            await YouTubeClient.Videos.Streams.DownloadAsync(audioOnlyStreamInfo, source);
        }
    }
}
