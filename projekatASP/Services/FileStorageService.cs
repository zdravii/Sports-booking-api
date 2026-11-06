using Application.Services;

namespace projekatASP.API.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        public FileStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public string SaveFile(IFormFile file, string subFolder)
        {
            string uploadsFolder = Path.Combine(_env.WebRootPath, subFolder);
            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string fullPath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);

            return $"/{subFolder}/{uniqueFileName}";
        }
    }
}
