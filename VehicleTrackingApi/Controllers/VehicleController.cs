﻿using System;
using System.Collections.Generic;
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
        public async Task<Models.ActionResult> Post(Vehicle vehicle)
        {
            Models.ActionResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    await VehicleManager.RegisterVehicle(vehicle);
                    result = new Models.ActionResult(HttpStatusCode.OK, vehicle);
                }
                else
                {
                    result = new Models.ActionResult(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (ArgumentException ex)
            {
                result = new Models.ActionResult(HttpStatusCode.BadRequest, ex.Message); // In the case of argument exception we will be getting meaning full error message.
            }
            catch (Exception)
            {
                result = new Models.ActionResult(HttpStatusCode.InternalServerError, "Internal server error has accured while registering the vehicle."); // no need to share implementation details with the consumer.
            }

            return result;
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
