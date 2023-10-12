using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.RestViewModels.ViewModels
{
    public class VehicleView
    {
        public string VehicleType { get; set; }
        public string VehicleBrand { get; set; }
        public int YearOfProduction { get; set; }
        public int TopSpeed { get; set; }
        public int VehicleMileage { get; set; }
        public string VehicleOwner { get; set; }
        public List<VehicleServiceHistoryView> VehicleServiceHistoryView { get; set; }
    }
}