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
    public interface IVehicleServiceHistoryService
    {
        Task<List<IVehicleServiceHistory>> GetVehicleHistoryServices();
        Task<List<IVehicleServiceHistory>> GetVehicleServiceHistoryById(Guid id);
        Task AddVehicleServiceHistory(VehicleServiceHistory vehicleServiceHistory);
        Task UpdateVehicleServiceHistory(Guid id, VehicleServiceHistory vehicleServiceHistory);
        Task DeleteVehicleServiceHistory(Guid id);
    }
}
