using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Service;
using WebProject.Model;
using WebProject.Model.ModelCommon;

namespace WebProject.Service.ServiceCommon
{
    public interface IVehicleService
    {
        Task<List<IVehicle>> GetVehicles(string vehicleType = null);

        Task<IVehicle> GetVehicleById(Guid id);

        Task AddVehicle(Vehicle vehicle);

        Task UpdateVehicle(Guid id, Vehicle updatedVehicle);

        Task DeleteVehicle(Guid id);
    }
}
