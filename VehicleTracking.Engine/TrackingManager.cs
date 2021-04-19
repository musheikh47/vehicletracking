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
    public class TrackingManager : IDisposable
    {
        #region Private Properties
        private ITrackingRepository _trackingRepository;
        private ITrackingRepository TrackingRepository
        {
            get
            {
                if (_trackingRepository == null)
                {
                    _trackingRepository = Factory.RepositoryFactory.GetRepository<ITrackingRepository>();
                }
                return _trackingRepository;
            }
        }
        #endregion

        #region Public Methods
        public async Task<bool> Record(Tracking trackingEntry)
        {
            try
            {
                trackingEntry.Time = DateTime.UtcNow.Ticks; // Using UTC time to normalize the time zone.
                await TrackingRepository.Create(trackingEntry);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error accured while recording tracking data for VehicleID:{trackingEntry.VehicleID}");
                throw; // Let the consumer decide what they want to do.
            }
           
        }
        #endregion

        #region IDisposible
        public void Dispose()
        {
            _trackingRepository?.Dispose();
        }
        #endregion
    }
}
