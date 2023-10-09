using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebProject.Model
{
    public class VehicleServiceHistory
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServiceDescription { get; set; }
        public decimal ServiceCost { get; set; }



        public Vehicle Vehicle { get; set; }
    }
}

