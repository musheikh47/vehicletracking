using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common.DTO;

namespace VehicleTracking.Engine
{
    public class VehicleManager : IDisposable
    {
        #region Constructor
        public VehicleManager()
        {

        }
        #endregion

        #region Public Methods
        public void RegisterVehicle(Vehicle vehicle)
        {

        }
        #endregion

        #region IDisposible Implementation
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
