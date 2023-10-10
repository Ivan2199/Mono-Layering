using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Data;
using WebProject.Model;
using WebProject.Model.ModelCommon;
using WebProject.Repository.RepositoryCommon;
using WebProject.Service.ServiceCommon;

namespace WebProject.Service
{
    public class VehicleServiceHistoryService : IVehicleServiceHistoryService
    {
        private readonly IVehicleServiceHistoryRepository _dataAccessVehicleServiceHistory;
        public VehicleServiceHistoryService(IVehicleServiceHistoryRepository dataAccessVehicleServiceHistory)
        {
            _dataAccessVehicleServiceHistory = dataAccessVehicleServiceHistory;
            _dataAccessVehicleServiceHistory.InitializeDatabase();
        }

        public async Task<List<IVehicleServiceHistory>> GetVehicleHistoryServices()
        {
            return await _dataAccessVehicleServiceHistory.GetVehicleHistoryServices();
        }

        public async Task<List<IVehicleServiceHistory>> GetVehicleServiceHistoryById(Guid id)
        {
            return await _dataAccessVehicleServiceHistory.GetVehicleServiceHistoryById(id);
        }

        public async Task AddVehicleServiceHistory(VehicleServiceHistory vehicleServiceHistory)
        {
            await _dataAccessVehicleServiceHistory.AddVehicleServiceHistory(vehicleServiceHistory);
        }
        public async Task UpdateVehicleServiceHistory(Guid id, VehicleServiceHistory vehicleServiceHistory)
        {
            await _dataAccessVehicleServiceHistory.UpdateVehicleServiceHistory(id, vehicleServiceHistory);
        }
        public async Task DeleteVehicleServiceHistory(Guid id)
        {
            await _dataAccessVehicleServiceHistory.DeleteVehicleServiceHistory(id);
        }
    }
}
