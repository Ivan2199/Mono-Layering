﻿using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebProject.App_Start;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Service;
using WebProject.Service.Common;
using WebProject.Repository;

namespace WebProject
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeDatabase();
            ContainerConfig.Configure();

        }

        private void InitializeDatabase()
        {
            var vehicle = new DataAccessVehicleRepository();
            vehicle.InitializeDatabaseAsync().GetAwaiter().GetResult();
            var vehicleServiceHistory = new DataAccessVehicleServiceHistory();
            vehicleServiceHistory.InitializeDatabaseAsync().GetAwaiter().GetResult();
        }
    }
}
