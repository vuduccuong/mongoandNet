using App.Domain.Entities;
using App.Domain.IRepositories;
using App.Domain.IServices;
using System.Threading.Tasks;

namespace App.Service.ImplementService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public Task<UserEntity> FinByEmail(string userEmail)
        {
            return Task.Run(() => _userRepo.FindOneAsync(u => u.Email == userEmail));
        }

        public Task Register(UserEntity user)
        {
            return Task.Run(() => _userRepo.InsertOneAsync(user));
        }
    }
}