using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProject.Service;
using WebProject.Model;

namespace WebProject.Controllers
{
    public class VehicleController : ApiController
    {
        private readonly VehicleService _vehicleService;

        public VehicleController()
        {
            _vehicleService = new VehicleService();
        }

        [HttpGet]
        public HttpResponseMessage Get(string vehicleType = null)
        {
            try
            {
                var vehicles = _vehicleService.GetVehicles(vehicleType);

                if (vehicles.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, "List is empty");
                }

                return Request.CreateResponse(HttpStatusCode.OK, vehicles);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var vehicle = _vehicleService.GetVehicleById(id);

                if (vehicle == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No Vehicle with that Id");
                }

                return Request.CreateResponse(HttpStatusCode.OK, vehicle);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Vehicle vehicle)
        {
            try
            {
                if (vehicle == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No data has been entered");
                }

                _vehicleService.AddVehicle(vehicle);

                return Request.CreateResponse(HttpStatusCode.Created, "Data has been entered successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Vehicle updatedVehicle)
        {
            try
            {
                var vehicleCurrent = _vehicleService.GetVehicleById(id);

                if (vehicleCurrent == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No vehicle with that id was found");
                }

                _vehicleService.UpdateVehicle(id, updatedVehicle);

                return Request.CreateResponse(HttpStatusCode.OK, "Data has been updated successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var vehicle = _vehicleService.GetVehicleById(id);

                if (vehicle == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No vehicle with that id was found");
                }

                _vehicleService.DeleteVehicle(id);

                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle has been deleted successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
