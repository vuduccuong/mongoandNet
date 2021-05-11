using App.Domain.Entities;
using App.Domain.IRepositories;
using App.Domain.IServices;
using System.Threading.Tasks;

namespace App.Service.ImplementService
{
    public class FileManagerService : IFileManagerService
    {
        private readonly IFileManagerRepository _fileRepo;

        public FileManagerService(IFileManagerRepository fileRepo)
        {
            _fileRepo = fileRepo;
        }

        public Task UploadFile(FileManagerEntity file)
        {
            return Task.Run(() => _fileRepo.InsertOneAsync(file));
        }
    }
}