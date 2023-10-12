using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProject.Service;
using System.Threading.Tasks;
using WebProject.Model;
using WebProject.Service.Common;
using WebProject.RestViewModels.RestModel;

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
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync()
        {
            var vehicleServiceHistories = await _dataAccessVehicleServiceHistory.GetVehicleHistoryServicesAsync();

            if (vehicleServiceHistories.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "List is empty");
            }

            return Request.CreateResponse(HttpStatusCode.OK, vehicleServiceHistories);
        }

        // GET api/VehicleServiceHistory/5
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            try
            {
                var vehicleServiceHistory = await _dataAccessVehicleServiceHistory.GetVehicleServiceHistoryByIdAsync(id);

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
        [HttpPost]
        public async Task<HttpResponseMessage> PostAsync([FromBody] VehicleServiceHistoryRest vehicleServiceHistoryRest)
        {
            try
            {
                if (vehicleServiceHistoryRest == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No data has been entered");
                }

                VehicleServiceHistory vehicleServiceHistory = MapVehicleServiceHistory(vehicleServiceHistoryRest);
                await _dataAccessVehicleServiceHistory.AddVehicleServiceHistoryAsync(vehicleServiceHistory);

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
        [HttpPut]
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] VehicleServiceHistoryRest vehicleServiceHistoryRest)
        {
            try
            {
                var existingVehicleServiceHistory = await _dataAccessVehicleServiceHistory.GetVehicleServiceHistoryByIdAsync(id);

                VehicleServiceHistory vehicleServiceHistory = MapVehicleServiceHistory(vehicleServiceHistoryRest);

                if (existingVehicleServiceHistory == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Vehicle Service History not found");
                }

                await _dataAccessVehicleServiceHistory.UpdateVehicleServiceHistoryAsync(id, vehicleServiceHistory);

                return Request.CreateResponse(HttpStatusCode.OK, vehicleServiceHistory);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/VehicleServiceHistory/5
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            try
            {
                var vehicleServiceHistory = await _dataAccessVehicleServiceHistory.GetVehicleServiceHistoryByIdAsync(id);

                if (vehicleServiceHistory == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Vehicle Service History not found");
                }

                await _dataAccessVehicleServiceHistory.DeleteVehicleServiceHistoryAsync(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        private VehicleServiceHistory MapVehicleServiceHistory(VehicleServiceHistoryRest vehicleServiceHistoryRest)
        {
            return new VehicleServiceHistory
            {
                Id = vehicleServiceHistoryRest.Id,
                VehicleId = vehicleServiceHistoryRest.VehicleId,
                ServiceDescription = vehicleServiceHistoryRest.ServiceDescription,
                ServiceDate = vehicleServiceHistoryRest.ServiceDate,
                ServiceCost = vehicleServiceHistoryRest.ServiceCost
            };
        }
    }
}
