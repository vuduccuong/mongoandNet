using App.Domain.Entities;
using System.Threading.Tasks;

namespace App.Domain.IServices
{
    public interface IUserService
    {
        Task Register(UserEntity user);

        Task<UserEntity> FinByEmail(string userEmail);
    }
}