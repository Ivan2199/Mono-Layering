using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Service;
using WebProject.Model;
using WebProject.Model.Common;

namespace WebProject.Service.Common
{
    public interface IVehicleServiceHistoryService
    {
        Task<List<IVehicleServiceHistory>> GetVehicleHistoryServicesAsync();
        Task<List<IVehicleServiceHistory>> GetVehicleServiceHistoryByIdAsync(Guid id);
        Task AddVehicleServiceHistoryAsync(VehicleServiceHistory vehicleServiceHistory);
        Task UpdateVehicleServiceHistoryAsync(Guid id, VehicleServiceHistory vehicleServiceHistory);
        Task DeleteVehicleServiceHistoryAsync(Guid id);
    }
}
