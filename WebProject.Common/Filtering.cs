using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebProject.Common
{
    public class Filtering
    {
        public string VehicleType {  get; set; }
        public string VehicleBrand { get; set; }
        public int MileageMin { get; set; }
        public int MileageMax {  get; set; }

        public Filtering() { }
        public Filtering(string vehicleType, string vehicleBrand, int mileageMin, int mileageMax)
        {
            VehicleType = vehicleType;
            VehicleBrand = vehicleBrand;
            MileageMin = mileageMin;
            MileageMax = mileageMax;
        }
    }
}
