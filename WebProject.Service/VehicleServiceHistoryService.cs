using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Data;
using WebProject.Model;
using WebProject.Model.Common;
using WebProject.Repository.Common;
using WebProject.Service.Common;

namespace WebProject.Service
{
    public class VehicleServiceHistoryService : IVehicleServiceHistoryService
    {
        private readonly IVehicleServiceHistoryRepository _dataAccessVehicleServiceHistory;
        public VehicleServiceHistoryService(IVehicleServiceHistoryRepository dataAccessVehicleServiceHistory)
        {
            _dataAccessVehicleServiceHistory = dataAccessVehicleServiceHistory;
        }

        public async Task<List<IVehicleServiceHistory>> GetVehicleHistoryServicesAsync()
        {
            return await _dataAccessVehicleServiceHistory.GetVehicleHistoryServices();
        }

        public async Task<List<IVehicleServiceHistory>> GetVehicleServiceHistoryByIdAsync(Guid id)
        {
            return await _dataAccessVehicleServiceHistory.GetVehicleServiceHistoryById(id);
        }

        public async Task AddVehicleServiceHistoryAsync(VehicleServiceHistory vehicleServiceHistory)
        {
            await _dataAccessVehicleServiceHistory.AddVehicleServiceHistory(vehicleServiceHistory);
        }
        public async Task UpdateVehicleServiceHistoryAsync(Guid id, VehicleServiceHistory vehicleServiceHistory)
        {
            await _dataAccessVehicleServiceHistory.UpdateVehicleServiceHistory(id, vehicleServiceHistory);
        }
        public async Task DeleteVehicleServiceHistoryAsync(Guid id)
        {
            await _dataAccessVehicleServiceHistory.DeleteVehicleServiceHistory(id);
        }
    }
}
