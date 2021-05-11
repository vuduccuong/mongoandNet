using App.Domain.Entities;
using System.Threading.Tasks;

namespace App.Domain.IServices
{
    public interface IFileManagerService
    {
        Task UploadFile(FileManagerEntity file);
    }
}