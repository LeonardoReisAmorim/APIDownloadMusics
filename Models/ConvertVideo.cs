using MediaToolkit;
using MediaToolkit.Model;

namespace APIDownloadMP3.Models
{
    public class ConvertVideo
    {
        public MediaFile InputFile { get; set; }
        public MediaFile OutputFile { get; set; }

        public ConvertVideo(string pathVideoMP4, string pathMp3)
        {
            if (string.IsNullOrWhiteSpace(pathVideoMP4) || string.IsNullOrWhiteSpace(pathMp3))
            {
                throw new ArgumentNullException();
            }

            InputFile = new MediaFile { Filename = pathVideoMP4 };
            OutputFile = new MediaFile { Filename = pathMp3 };
        }

        public void ConvertMP4ToMP3()
        {
            using (var engine = new Engine())
            {
                engine.GetMetadata(InputFile);

                engine.Convert(InputFile, OutputFile);
            }
        }
    }
}