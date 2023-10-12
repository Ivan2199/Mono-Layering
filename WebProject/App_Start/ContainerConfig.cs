using Autofac;
using Autofac.Integration.WebApi;
using System.Web.Http;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Repository.Common;
using WebProject.Service;
using WebProject.Service.Common;

namespace WebProject.App_Start
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<VehicleController>();
            builder.RegisterType<VehicleService>().As<IVehicleService>();
            builder.RegisterType<DataAccessVehicleRepository>().As<IVehicleRepository>();

            builder.RegisterType<VehicleServiceHistoryController>();
            builder.RegisterType<VehicleServiceHistoryService>().As<IVehicleServiceHistoryService>();
            builder.RegisterType<DataAccessVehicleServiceHistory>().As<IVehicleServiceHistoryRepository>();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
