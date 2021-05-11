using App.Domain.Entities;
using App.Domain.IRepositories;
using App.Domain.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.ImplementService
{
    public class ShipwreckService : IShipwreckService
    {
        private readonly IShipwreckRepository _shipwreckRepo;

        public ShipwreckService(IShipwreckRepository shipwreckRepo)
        {
            _shipwreckRepo = shipwreckRepo;
        }

        public async Task<IEnumerable<ShipwreckEntity>> GetAll()
        {
            return await _shipwreckRepo.FilterByAsync(x => x.FeautureType == "Wrecks - Visible");
        }
    }
}