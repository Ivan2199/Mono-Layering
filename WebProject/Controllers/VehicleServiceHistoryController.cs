using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using WebProject.Service;
using WebProject.Model;
using WebProject.Service.ServiceCommon;

namespace WebProject.Controllers
{
    public class VehicleServiceHistoryController : ApiController
    {
        private readonly IVehicleServiceHistoryService _dataAccessVehicleServiceHistory;

        public VehicleServiceHistoryController(IVehicleServiceHistoryService dataAccessVehicleServiceHistory)
        {
            _dataAccessVehicleServiceHistory = dataAccessVehicleServiceHistory;
        }

        // GET api/VehicleServiceHistory
        public async Task<HttpResponseMessage> Get()
        {
            var vehicleServiceHistories = await _dataAccessVehicleServiceHistory.GetVehicleHistoryServices();

            if (vehicleServiceHistories.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "List is empty");
            }

            return Request.CreateResponse(HttpStatusCode.OK, vehicleServiceHistories);
        }

        // GET api/VehicleServiceHistory/5
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            try
            {
                var vehicleServiceHistory = await _dataAccessVehicleServiceHistory.GetVehicleServiceHistoryById(id);

                if (vehicleServiceHistory == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No Vehicle Service History with that Id");
                }

                return Request.CreateResponse(HttpStatusCode.OK, vehicleServiceHistory);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/VehicleServiceHistory
        public async Task<HttpResponseMessage> Post([FromBody] VehicleServiceHistory vehicleServiceHistory)
        {
            try
            {
                await _dataAccessVehicleServiceHistory.AddVehicleServiceHistory(vehicleServiceHistory);

                var response = Request.CreateResponse(HttpStatusCode.Created, vehicleServiceHistory);
                response.Headers.Location = new Uri(Request.RequestUri, $"/api/VehicleServiceHistory/{vehicleServiceHistory.Id}");
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/VehicleServiceHistory/5
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] VehicleServiceHistory vehicleServiceHistory)
        {
            try
            {
                var existingVehicleServiceHistory = await _dataAccessVehicleServiceHistory.GetVehicleServiceHistoryById(id);

                if (existingVehicleServiceHistory == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Vehicle Service History not found");
                }

                await _dataAccessVehicleServiceHistory.UpdateVehicleServiceHistory(id, vehicleServiceHistory);

                return Request.CreateResponse(HttpStatusCode.OK, vehicleServiceHistory);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/VehicleServiceHistory/5
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            try
            {
                var vehicleServiceHistory = await _dataAccessVehicleServiceHistory.GetVehicleServiceHistoryById(id);

                if (vehicleServiceHistory == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Vehicle Service History not found");
                }

                await _dataAccessVehicleServiceHistory.DeleteVehicleServiceHistory(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
