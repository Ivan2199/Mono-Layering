using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Model;

namespace WebProject.Repository.RepositoryCommon
{
    public interface IVehicleRepository
    {
        void CloseConnection();

        void InitializeDatabase();

        List<Vehicle> GetVehicles(string vehicleType = null);

        Vehicle GetVehicleById(int id);

        void AddVehicle(Vehicle vehicle);

        void UpdateVehicle(int id, Vehicle updatedVehicle);

        void DeleteVehicle(int id);
    }
}
