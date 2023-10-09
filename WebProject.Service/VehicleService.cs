using System;
using System.Collections.Generic;
using WebProject.Data;
using WebProject.Model;
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

        public List<Vehicle> GetVehicles(string vehicleType = null)
        {
            try
            {
                return _dataAccess.GetVehicles(vehicleType);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }

        public Vehicle GetVehicleById(int id)
        {
            try
            {
                return _dataAccess.GetVehicleById(id);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            try
            {
                _dataAccess.AddVehicle(vehicle);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }

        public void UpdateVehicle(int id, Vehicle updatedVehicle)
        {
            try
            {
                _dataAccess.UpdateVehicle(id, updatedVehicle);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }

        public void DeleteVehicle(int id)
        {
            try
            {
                _dataAccess.DeleteVehicle(id);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }
    }
}
