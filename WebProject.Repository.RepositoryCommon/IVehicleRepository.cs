using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Common;
using WebProject.Model;
using WebProject.Model.Common;

namespace WebProject.Repository.Common
{
    public interface IVehicleRepository
    {

        Task InitializeDatabaseAsync();

        Task<List<IVehicle>> GetVehiclesAsync(Paging paging, Sorting sorting, Filtering filtering);

        Task<IVehicle> GetVehicleByIdAsync(Guid id);

        Task AddVehicleAsync(IVehicle vehicle);

        Task UpdateVehicleAsync(Guid id, IVehicle updatedVehicle);

        Task DeleteVehicleAsync(Guid id);
    }
}
