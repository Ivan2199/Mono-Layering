using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Model;

namespace WebProject.Repository.RepositoryCommon
{
    public interface IVehicleServiceHistoryRepository
    {
        Task InitializeDatabase();
        Task<List<VehicleServiceHistory>> GetVehicleHistoryServices();
        Task<List<VehicleServiceHistory>> GetVehicleServiceHistoryById(Guid id);
        Task AddVehicleServiceHistory(VehicleServiceHistory vehicleServiceHistory);
        Task UpdateVehicleServiceHistory(Guid id, VehicleServiceHistory vehicleServiceHistory);
        Task DeleteVehicleServiceHistory(Guid id);
    }
}
