using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common;
using VehicleTracking.Common.DTO;
using VehicleTracking.Common.Interfaces;

namespace VehicleTracking.Engine
{
    public class VehicleManager : IDisposable
    {
        #region Private Properties
        private IVehicleRepository _vehicleRepository;       
        #endregion

        #region Constructor
        public VehicleManager(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        #endregion

        #region Public Methods
        public async Task RegisterVehicle(Vehicle vehicle)
        {
            try
            {
                vehicle.RegDate = DateTime.UtcNow.Ticks; // Using UTC time to normalize the time zone.
                vehicle.ID = await _vehicleRepository.Create(vehicle);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error accured while registering vehicle. RegNum:{vehicle.RegNumber}");
                throw; // Let the consumer decide what they want to do.
            }
        }
        #endregion

        #region IDisposible Implementation
        public void Dispose()
        {
            _vehicleRepository?.Dispose();
        }
        #endregion
    }
}
