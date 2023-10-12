using System;
using System.Collections.Generic;
using WebProject.Model;

namespace WebProject.Model.Common
{
    public interface IVehicle
    {
        Guid Id { get; set; }
        int TopSpeed { get; set; }
        string VehicleBrand { get; set; }
        int VehicleMileage { get; set; }
        string VehicleOwner { get; set; }
        List<IVehicleServiceHistory> VehicleServiceHistory { get; set; }
        string VehicleType { get; set; }
        int YearOfProduction { get; set; }
    }
}