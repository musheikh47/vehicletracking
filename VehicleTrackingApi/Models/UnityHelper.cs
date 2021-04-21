using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using VehicleTracking.Common.Interfaces;
using VehicleTracking.DataRepository.Repositories;

namespace VehicleTrackingApi.Models
{
    public static class UnityHelper
    {
        public static UnityContainer Container { private set; get; }

        public static void RegisterTypes()
        {
            Container = new UnityContainer();
            Container.RegisterType<IUserRepository,UserRepository>();
            Container.RegisterType<IVehicleRepository, VehicleRepository>();
            Container.RegisterType<ITrackingRepository, TrackingRepository>();
        }

    }
}