using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
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
      
        // POST: api/Vehicle
        public async Task<Models.ActionResult<Vehicle>> Post(Vehicle vehicle)
        {
            Models.ActionResult<Vehicle> result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    await VehicleManager.RegisterVehicle(vehicle);
                    result = new Models.ActionResult<Vehicle>(HttpStatusCode.OK, vehicle);
                }
                else
                {
                    result = new Models.ActionResult<Vehicle>(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (ArgumentException ex)
            {
                result = new Models.ActionResult<Vehicle>(HttpStatusCode.BadRequest, ex.Message); // In the case of argument exception we will be getting meaning full error message.
            }
            catch (Exception)
            {
                result = new Models.ActionResult<Vehicle>(HttpStatusCode.InternalServerError, "Internal server error has accured while registering the vehicle."); // no need to share implementation details with the consumer.
            }

            return result;
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
