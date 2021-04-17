using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.DataRepository.Repositories
{
    public class BaseRepository : IDisposable
    {
        #region Private Properties
        private VehicleTrackingDBEntities _dbContext;
        #endregion

        #region Protected Properties
        protected VehicleTrackingDBEntities DbContext
        {
            get
            {
                if (_dbContext == null)
                    _dbContext = new VehicleTrackingDBEntities();

                return _dbContext;
            }

        }
        #endregion
        #region Constructor
        public BaseRepository()
        {

        }
        public BaseRepository(VehicleTrackingDBEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
        #endregion
    }
}
