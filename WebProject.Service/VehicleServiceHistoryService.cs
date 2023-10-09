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

        public List<VehicleServiceHistory> GetVehicleHistoryServices()
        {
            try
            {
                return _dataAccessVehicleServiceHistory.GetVehicleHistoryServices();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<VehicleServiceHistory> GetVehicleServiceHistoryById(int id)
        {
            return _dataAccessVehicleServiceHistory.GetVehicleServiceHistoryById(id);
        }

        public void AddVehicleServiceHistory(VehicleServiceHistory vehicleServiceHistory)
        {
            _dataAccessVehicleServiceHistory.AddVehicleServiceHistory(vehicleServiceHistory);
        }
        public void UpdateVehicleServiceHistory(int id, VehicleServiceHistory vehicleServiceHistory)
        {
            _dataAccessVehicleServiceHistory.UpdateVehicleServiceHistory(id, vehicleServiceHistory);
        }
        public void DeleteVehicleServiceHistory(int id)
        {
            _dataAccessVehicleServiceHistory.DeleteVehicleServiceHistory(id);
        }
    }
}
