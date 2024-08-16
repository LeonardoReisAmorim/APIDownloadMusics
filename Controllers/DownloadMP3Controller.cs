using APIDownloadMP3.DTO;
using APIDownloadMP3.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIDownloadMP3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadMP3Controller : ControllerBase
    {
        private readonly ILogger<DownloadMP3Controller> _logger;

        public DownloadMP3Controller(ILogger<DownloadMP3Controller> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> DownloadVideosYoutube([FromBody] YoutubeVideos videos)
        {
            var convertVideos = new ConvertVideoToAudio(videos.UrlsYoutube);
            var fileContent = await convertVideos.ConverterVideoToAudio();

            return File(fileContent, "application/zip", "musicas.zip");
        }
    }
}
