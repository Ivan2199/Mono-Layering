using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject.Model.Common;

namespace WebProject.Model
{
    public class Vehicle : IVehicle
    {
        public Guid Id { get; set; }
        public string VehicleType { get; set; }
        public string VehicleBrand { get; set; }
        public int YearOfProduction { get; set; }
        public int TopSpeed { get; set; }
        public int VehicleMileage { get; set; }
        public string VehicleOwner { get; set; }

        public List<IVehicleServiceHistory> VehicleServiceHistory { get; set; }
    }

}
