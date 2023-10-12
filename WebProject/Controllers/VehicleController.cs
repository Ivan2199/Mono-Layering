using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProject.Service;
using System.Threading.Tasks;
using WebProject.Model;
using WebProject.Service.Common;
using AutoMapper;
using WebProject.RestViewModels.RestModel;
using WebProject.RestViewModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;
using WebProject.Model.Common;
using WebProject.Common;

namespace WebProject.Controllers
{
    public class VehicleController : ApiController
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(int pageNumber = 1, int pageSize= 10, string orderBy = "YearOfProduction", string sortOrder = "ASC",string vehicleType = "", string vehicleBrand = "", int mileageMin = 0, int mileageMax = 0)
        {
            try
            {
                Paging paging = new Paging(pageNumber, pageSize);
                Sorting sorting = new Sorting(orderBy, sortOrder);
                Filtering filtering = new Filtering(vehicleType, vehicleBrand, mileageMin, mileageMax);
                var vehicles = await _vehicleService.GetVehiclesAsync(paging, sorting, filtering);

                if (vehicles.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, "List is empty");
                }
                
                List<VehicleView> vehiclesView = new List<VehicleView>();
                foreach (var vehicle in vehicles)
                {
                    vehiclesView.Add(MapVehicleView(vehicle));
                }

                return Request.CreateResponse(HttpStatusCode.OK, vehiclesView);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

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
        public async Task<HttpResponseMessage> PostAsync([FromBody] VehicleRest vehicleRest)
        {
            try
            {
                if (vehicleRest == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No data has been entered");
                }

                Vehicle vehicle = MapVehicle(vehicleRest);

                await _vehicleService.AddVehicleAsync(vehicle);

                return Request.CreateResponse(HttpStatusCode.Created, "Data has been entered successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] VehicleRest vehicleRest)
        {
            try
            {
                var vehicleCurrent = await _vehicleService.GetVehicleByIdAsync(id);

                Vehicle vehicle = MapVehicle(vehicleRest); 

                if (vehicleCurrent == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No vehicle with that id was found");
                }

                await _vehicleService.UpdateVehicleAsync(id, vehicle);

                return Request.CreateResponse(HttpStatusCode.OK, "Data has been updated successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

                if (vehicle == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No vehicle with that id was found");
                }

                await _vehicleService.DeleteVehicleAsync(id);

                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle has been deleted successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private Vehicle MapVehicle(VehicleRest vehicleRest)
        {
            return new Vehicle
            {
                Id = vehicleRest.Id,
                VehicleType = vehicleRest.VehicleType,
                VehicleBrand = vehicleRest.VehicleBrand,
                TopSpeed = vehicleRest.TopSpeed,
                VehicleMileage = vehicleRest.VehicleMileage,
                VehicleOwner = vehicleRest.VehicleOwner,
                YearOfProduction = vehicleRest.YearOfProduction
            };
        }
        private VehicleView MapVehicleView(IVehicle vehicle)
        {
            return new VehicleView
            {
                VehicleType = vehicle.VehicleType,
                VehicleBrand = vehicle.VehicleBrand,
                TopSpeed = vehicle.TopSpeed,
                VehicleMileage = vehicle.VehicleMileage,
                YearOfProduction = vehicle.YearOfProduction,
                VehicleOwner = vehicle.VehicleOwner,
                VehicleServiceHistoryView = vehicle.VehicleServiceHistory.Select(MapVehicleServiceHistoryView).ToList()
            };
        }
        private VehicleServiceHistoryView MapVehicleServiceHistoryView(IVehicleServiceHistory history)
        {
            return new VehicleServiceHistoryView
            {
                ServiceDescription = history.ServiceDescription,
                ServiceDate = history.ServiceDate,
                ServiceCost = history.ServiceCost
            };
        }


    }
}
