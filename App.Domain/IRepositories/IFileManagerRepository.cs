using App.Domain.Base;
using App.Domain.Entities;

namespace App.Domain.IRepositories
{
    public interface IFileManagerRepository : IMongoRepositoryBase<FileManagerEntity>
    {
    }
}