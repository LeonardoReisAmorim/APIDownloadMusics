using System.IO.Compression;

namespace DownloadMP3
{
    public static class FileUtils
    {
        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public static void CreateZipAllMusics(string sourceDirectoryName, string destinationArchiveFilename)
        {
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFilename);
        }

        public static MemoryStream GetStreamZipAsync(string zipPath, string zipFileName)
        {
            var zipFilePath = Directory.GetFiles(zipPath, zipFileName, SearchOption.TopDirectoryOnly).First();
            var allBytesZip = File.ReadAllBytes(zipFilePath);
            return new MemoryStream(allBytesZip);
        }
    }
}
