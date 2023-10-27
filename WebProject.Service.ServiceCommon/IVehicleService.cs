using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Service;
using WebProject.Model;
using WebProject.Model.Common;
using WebProject.Common;

namespace WebProject.Service.Common
{
    public interface IVehicleService
    {
        Task<(List<IVehicle>, Paging)> GetVehiclesAsync(Paging paging, Sorting sorting, Filtering filtering);

        Task<IVehicle> GetVehicleByIdAsync(Guid id);

        Task AddVehicleAsync(Vehicle vehicle);

        Task UpdateVehicleAsync(Guid id, Vehicle updatedVehicle);

        Task DeleteVehicleAsync(Guid id);
    }
}
