using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Service;
using WebProject.Model;

namespace WebProject.Service.ServiceCommon
{
    public interface IVehicleServiceHistoryService
    {
        Task<List<VehicleServiceHistory>> GetVehicleHistoryServices();
        Task<List<VehicleServiceHistory>> GetVehicleServiceHistoryById(Guid id);
        Task AddVehicleServiceHistory(VehicleServiceHistory vehicleServiceHistory);
        Task UpdateVehicleServiceHistory(Guid id, VehicleServiceHistory vehicleServiceHistory);
        Task DeleteVehicleServiceHistory(Guid id);
    }
}
