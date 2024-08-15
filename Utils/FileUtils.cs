namespace DownloadMP3
{
    public static class FileUtils
    {
        public static async Task WriteAllBytesAsync(string source, byte[] bytes)
        {
            await File.WriteAllBytesAsync(source, bytes);
        }

        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
