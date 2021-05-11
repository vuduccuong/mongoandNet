using App.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.IServices
{
    public interface IShipwreckService
    {
        Task<IEnumerable<ShipwreckEntity>> GetAll();
    }
}