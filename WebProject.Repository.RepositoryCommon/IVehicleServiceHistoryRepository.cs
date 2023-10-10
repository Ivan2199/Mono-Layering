using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Model;
using WebProject.Model.ModelCommon;

namespace WebProject.Repository.RepositoryCommon
{
    public interface IVehicleServiceHistoryRepository
    {
        Task InitializeDatabase();
        Task<List<IVehicleServiceHistory>> GetVehicleHistoryServices();
        Task<List<IVehicleServiceHistory>> GetVehicleServiceHistoryById(Guid id);
        Task AddVehicleServiceHistory(IVehicleServiceHistory vehicleServiceHistory);
        Task UpdateVehicleServiceHistory(Guid id, IVehicleServiceHistory vehicleServiceHistory);
        Task DeleteVehicleServiceHistory(Guid id);
    }
}
