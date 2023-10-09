using System;
using System.Collections.Generic;
using WebProject.Data;
using WebProject.Model;
using System.Threading.Tasks;
using WebProject.Service.ServiceCommon;

namespace WebProject.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly DataAccess _dataAccess;

        public VehicleService()
        {
            _dataAccess = new DataAccess();
            _dataAccess.InitializeDatabase();
        }

        public async Task<List<Vehicle>> GetVehicles(string vehicleType = null)
        {
            return await _dataAccess.GetVehicles(vehicleType);
        }

        public async Task<Vehicle> GetVehicleById(Guid id)
        {
             return await _dataAccess.GetVehicleById(id);
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            await _dataAccess.AddVehicle(vehicle);
        }

        public async Task UpdateVehicle(Guid id, Vehicle updatedVehicle)
        {
            await _dataAccess.UpdateVehicle(id, updatedVehicle);
        }

        public async Task DeleteVehicle(Guid id)
        {
             await _dataAccess.DeleteVehicle(id);
        }
    }
}
