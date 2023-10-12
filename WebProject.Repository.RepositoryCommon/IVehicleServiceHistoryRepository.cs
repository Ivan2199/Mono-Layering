using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Model;
using WebProject.Model.Common;

namespace WebProject.Repository.Common
{
    public interface IVehicleServiceHistoryRepository
    {
        Task InitializeDatabaseAsync();
        Task<List<IVehicleServiceHistory>> GetVehicleHistoryServices();
        Task<List<IVehicleServiceHistory>> GetVehicleServiceHistoryById(Guid id);
        Task AddVehicleServiceHistory(IVehicleServiceHistory vehicleServiceHistory);
        Task UpdateVehicleServiceHistory(Guid id, IVehicleServiceHistory vehicleServiceHistory);
        Task DeleteVehicleServiceHistory(Guid id);
    }
}
