using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VehicleTracking.Common.DTO;
using VehicleTracking.Engine;

namespace VehicleTrackingApi.Controllers
{
    public class VehicleController : ApiController
    {
        private VehicleManager _vehicleManager;
        public VehicleManager VehicleManager
        {
            get
            {
                if (_vehicleManager == null)
                {
                    _vehicleManager = new VehicleManager();
                }
                return _vehicleManager;
            }
        }
        // GET: api/Vehicle
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Vehicle/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Vehicle
        public void Post(Vehicle vehicle)
        {
            VehicleManager.RegisterVehicle(vehicle);
        }

        // PUT: api/Vehicle/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Vehicle/5
        public void Delete(int id)
        {
        }

        #region Override
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _vehicleManager?.Dispose();
        }
        #endregion
    }
}
