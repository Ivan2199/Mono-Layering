using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.RestViewModels.ViewModels
{
    public class VehicleView
    {
        public Guid Id { get; set; }
        public string VehicleType { get; set; }
        public string VehicleBrand { get; set; }
        public int YearOfProduction { get; set; }
        public int TopSpeed { get; set; }
        public int VehicleMileage { get; set; }
        public string VehicleOwner { get; set; }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalSize { get; set; }
        public List<VehicleServiceHistoryView> VehicleServiceHistoryView { get; set; }
    }
}