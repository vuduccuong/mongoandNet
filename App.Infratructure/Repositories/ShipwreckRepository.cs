using App.Domain.Base;
using App.Domain.Entities;
using App.Domain.IRepositories;

namespace App.Infratructure.Repositories
{
    public class ShipwreckRepository : MongoRepositoryBase<ShipwreckEntity>, IShipwreckRepository
    {
        public ShipwreckRepository(IMongoDBSettings settings) : base(settings)
        {
        }
    }
}