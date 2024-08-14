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
            if(String.IsNullOrWhiteSpace(videos.UrlsYoutube))
            {
                return BadRequest("Necessário enviar os dados");
            }

            var lengthMusics = await ConvertVideoToAudio.ConverterVideoToAudio(videos.UrlsYoutube);

            return Ok(lengthMusics);
        }
    }
}
