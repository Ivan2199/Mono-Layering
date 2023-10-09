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
            try
            {
                return await _dataAccess.GetVehicles(vehicleType);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }

        public async Task<Vehicle> GetVehicleById(Guid id)
        {
            try
            {
                return await _dataAccess.GetVehicleById(id);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            try
            {
                await _dataAccess.AddVehicle(vehicle);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }

        public async Task UpdateVehicle(Guid id, Vehicle updatedVehicle)
        {
            try
            {
                await _dataAccess.UpdateVehicle(id, updatedVehicle);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }

        public async Task DeleteVehicle(Guid id)
        {
            try
            {
                await _dataAccess.DeleteVehicle(id);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }
    }
}
