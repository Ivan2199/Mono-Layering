using System;
using System.Collections.Generic;
using WebProject.Data;
using WebProject.Model;
using System.Threading.Tasks;
using WebProject.Service.Common;
using WebProject.Repository.Common;
using WebProject.Model.Common;
using WebProject.Common;

namespace WebProject.Service
{
    public class VehicleService : IVehicleService
    {
       
        private readonly IVehicleRepository _dataAccess;

        public VehicleService(IVehicleRepository dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<List<IVehicle>> GetVehiclesAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            return await _dataAccess.GetVehiclesAsync(paging, sorting, filtering);
        }

        public async Task<IVehicle> GetVehicleByIdAsync(Guid id)
        {
             return await _dataAccess.GetVehicleByIdAsync(id);
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await _dataAccess.AddVehicleAsync(vehicle);
        }

        public async Task UpdateVehicleAsync(Guid id, Vehicle updatedVehicle)
        {
            await _dataAccess.UpdateVehicleAsync(id, updatedVehicle);
        }

        public async Task DeleteVehicleAsync(Guid id)
        {
             await _dataAccess.DeleteVehicleAsync(id);
        }
    }
}
