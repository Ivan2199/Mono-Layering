using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.RestViewModels.ViewModels
{
    public class VehicleServiceHistoryView
    {
        public DateTime ServiceDate { get; set; }
        public string ServiceDescription { get; set; }
        public decimal ServiceCost { get; set; }
    }
}