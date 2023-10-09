using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Model;

namespace WebProject.Repository.RepositoryCommon
{
    public interface IVehicleRepository
    {

        Task InitializeDatabase();

        Task<List<Vehicle>> GetVehicles(string vehicleType = null);

        Task<Vehicle> GetVehicleById(Guid id);

        Task AddVehicle(Vehicle vehicle);

        Task UpdateVehicle(Guid id, Vehicle updatedVehicle);

        Task DeleteVehicle(Guid id);
    }
}
