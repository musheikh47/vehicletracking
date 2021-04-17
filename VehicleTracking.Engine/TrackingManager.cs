using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #region IDisposible
        public void Dispose()
        {
            _trackingRepository?.Dispose();
        }
        #endregion
    }
}
