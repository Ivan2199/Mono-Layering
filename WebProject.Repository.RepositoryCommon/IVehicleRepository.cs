using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Model;
using WebProject.Model.ModelCommon;

namespace WebProject.Repository.RepositoryCommon
{
    public interface IVehicleRepository
    {

        Task InitializeDatabase();

        Task<List<IVehicle>> GetVehicles(string vehicleType = null);

        Task<IVehicle> GetVehicleById(Guid id);

        Task AddVehicle(IVehicle vehicle);

        Task UpdateVehicle(Guid id, IVehicle updatedVehicle);

        Task DeleteVehicle(Guid id);
    }
}
