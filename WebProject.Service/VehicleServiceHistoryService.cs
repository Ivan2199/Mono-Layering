using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Data;
using WebProject.Model;
using WebProject.Service.ServiceCommon;

namespace WebProject.Service
{
    public class VehicleServiceHistoryService : IVehicleServiceHistoryService
    {
        private readonly DataAccessVehicleServiceHistory _dataAccessVehicleServiceHistory;
        public VehicleServiceHistoryService()
        {
            _dataAccessVehicleServiceHistory = new DataAccessVehicleServiceHistory();
            _dataAccessVehicleServiceHistory.InitializeDatabase();
        }

        public async Task<List<VehicleServiceHistory>> GetVehicleHistoryServices()
        {
            return await _dataAccessVehicleServiceHistory.GetVehicleHistoryServices();
        }

        public async Task<List<VehicleServiceHistory>> GetVehicleServiceHistoryById(Guid id)
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
