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
        void InitializeDatabase();
        List<VehicleServiceHistory> GetVehicleHistoryServices();
        List<VehicleServiceHistory> GetVehicleServiceHistoryById(int id);
        void AddVehicleServiceHistory(VehicleServiceHistory vehicleServiceHistory);
        void UpdateVehicleServiceHistory(int id, VehicleServiceHistory vehicleServiceHistory);
        void DeleteVehicleServiceHistory(int id);
    }
}
