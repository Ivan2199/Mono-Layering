using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProject.Service;
using System.Threading.Tasks;
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
        public async Task<HttpResponseMessage> Get(string vehicleType = null)
        {
            try
            {
                var vehicles = await _vehicleService.GetVehicles(vehicleType);

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
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleById(id);

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
        public async Task<HttpResponseMessage> Post([FromBody] Vehicle vehicle)
        {
            try
            {
                if (vehicle == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No data has been entered");
                }

                await _vehicleService.AddVehicle(vehicle);

                return Request.CreateResponse(HttpStatusCode.Created, "Data has been entered successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] Vehicle updatedVehicle)
        {
            try
            {
                var vehicleCurrent = await _vehicleService.GetVehicleById(id);

                if (vehicleCurrent == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No vehicle with that id was found");
                }

                await _vehicleService.UpdateVehicle(id, updatedVehicle);

                return Request.CreateResponse(HttpStatusCode.OK, "Data has been updated successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleById(id);

                if (vehicle == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No vehicle with that id was found");
                }

                await _vehicleService.DeleteVehicle(id);

                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle has been deleted successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
