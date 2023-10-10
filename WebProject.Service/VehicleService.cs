using System;
using System.Collections.Generic;
using WebProject.Data;
using WebProject.Model;
using System.Threading.Tasks;
using WebProject.Service.ServiceCommon;
using WebProject.Repository.RepositoryCommon;
using WebProject.Model.ModelCommon;

namespace WebProject.Service
{
    public class VehicleService : IVehicleService
    {
       
        private readonly IVehicleRepository _dataAccess;

        public VehicleService(IVehicleRepository dataAccess)
        {
            _dataAccess = dataAccess;
            _dataAccess.InitializeDatabase();
        }

        public async Task<List<IVehicle>> GetVehicles(string vehicleType = null)
        {
            return await _dataAccess.GetVehicles(vehicleType);
        }

        public async Task<IVehicle> GetVehicleById(Guid id)
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
