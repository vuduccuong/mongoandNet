using App.Domain.Base;
using App.Domain.Entities;
using App.Domain.IRepositories;

namespace App.Infratructure.Repositories
{
    public class UserRepository : MongoRepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(IMongoDBSettings settings) : base(settings)
        {
        }
    }
}