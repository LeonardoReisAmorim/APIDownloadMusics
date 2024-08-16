namespace APIDownloadMP3.Utils
{
    public class DirectoryUtils
    {
        public string RootPath { get; set; }
        public string FilesPath { get; set; }
        public string ZipPath { get; set; }
        public string ZipFileName { get; set; }

        public DirectoryUtils()
        {
            RootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads", Guid.NewGuid().ToString());
            FilesPath = Path.Combine(RootPath, "files");
            ZipPath = Path.Combine(RootPath, "zip");
            ZipFileName = "Resultado.zip";

            CreateDirectory();
        }

        private void CreateDirectory()
        {
            Directory.CreateDirectory(FilesPath);
            Directory.CreateDirectory(ZipPath);
        }

        public void DeleteDirectoryRoot()
        {
            if (Directory.Exists(RootPath))
            {
                Directory.Delete(RootPath, true);
            }
        }
    }
}
