using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Service;
using WebProject.Model;


namespace WebProject.Service.ServiceCommon
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetVehicles(string vehicleType = null);

        Task<Vehicle> GetVehicleById(Guid id);

        Task AddVehicle(Vehicle vehicle);

        Task UpdateVehicle(Guid id, Vehicle updatedVehicle);

        Task DeleteVehicle(Guid id);
    }
}
