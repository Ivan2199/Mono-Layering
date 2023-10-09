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
        List<VehicleServiceHistory> GetVehicleHistoryServices();
        List<VehicleServiceHistory> GetVehicleServiceHistoryById(int id);
        void AddVehicleServiceHistory(VehicleServiceHistory vehicleServiceHistory);
        void UpdateVehicleServiceHistory(int id, VehicleServiceHistory vehicleServiceHistory);
        void DeleteVehicleServiceHistory(int id);
    }
}
