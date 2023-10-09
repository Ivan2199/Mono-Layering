using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Service;
using WebProject.Model;


namespace WebProject.Service.ServiceCommon
{
    public interface IVehicleService
    {
        List<Vehicle> GetVehicles(string vehicleType = null);

        Vehicle GetVehicleById(int id);

        void AddVehicle(Vehicle vehicle);

        void UpdateVehicle(int id, Vehicle updatedVehicle);

        void DeleteVehicle(int id);
    }
}
