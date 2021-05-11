using App.Domain.Base;
using App.Domain.Entities;
using App.Domain.IRepositories;

namespace App.Infratructure.Repositories
{
    public class FileManagerRepository : MongoRepositoryBase<FileManagerEntity>, IFileManagerRepository
    {
        public FileManagerRepository(IMongoDBSettings settings) : base(settings)
        {
        }
    }
}