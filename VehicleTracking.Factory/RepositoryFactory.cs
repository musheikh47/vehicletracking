using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common.Interfaces;

namespace VehicleTracking.Factory
{
    public static class RepositoryFactory
    {
        // VehicleTracing.DataRepository is tightly coupled with EFModel. I want to remove this dependency. It will help us to change the technology in future. 
        // For unit testing we can return mock repository from this point. In our system only this project will be aware that we are using EFModel. All other projects will be using interfaces.
        public static T GetRepository<T>() where T : class
        {
            var repository =  default(T);
            var type = typeof(T);
            if (type == typeof(IVehicleRepository)) // Unity container is the better way to resolve dependencies. 
                repository = (new VehicleTracking.DataRepository.Repositories.VehicleRepository()) as T;
            else if (type == typeof(ITrackingRepository))
                repository = (new VehicleTracking.DataRepository.Repositories.TrackingRepository()) as T;
            return repository;
        }
    }
}
