using System;

namespace WebProject.Model.Common
{
    public interface IVehicleServiceHistory
    {
        Guid Id { get; set; }
        decimal ServiceCost { get; set; }
        DateTime ServiceDate { get; set; }
        string ServiceDescription { get; set; }
        IVehicle Vehicle { get; set; }
        Guid VehicleId { get; set; }
    }
}