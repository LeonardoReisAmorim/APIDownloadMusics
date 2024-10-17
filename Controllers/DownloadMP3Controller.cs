using APIDownloadMP3.DTO;
using APIDownloadMP3.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIDownloadMP3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadMP3Controller : ControllerBase
    {
        public DownloadMP3Controller()
        {
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
