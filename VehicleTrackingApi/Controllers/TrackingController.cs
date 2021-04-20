using System;
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
    public class TrackingController : ApiController
    {
        private TrackingManager _trackingManager;
        public TrackingManager TrackingManager
        {
            get
            {
                if (_trackingManager == null)
                {
                    _trackingManager = new TrackingManager();
                }
                return _trackingManager;
            }
        }
        // GET: api/Tracking/5
        /// <summary>
        /// This method will return last recorded position of the vehicle
        /// </summary>
        /// <param name="id">VehicleID</param>
        /// <returns></returns>
        public async Task<Models.ActionResult> Get(int id)
        {
            Models.ActionResult result = null;
            try
            {
                if (id > 0)
                {
                    var trackingEntry = await TrackingManager.GetLastTrackingRecord(id);
                    result = new Models.ActionResult(HttpStatusCode.OK, trackingEntry);
                }
                else
                {
                    result = new Models.ActionResult(HttpStatusCode.BadRequest,"id should be greater than zero.");
                }
            }
            catch (ArgumentException ex)
            {
                result = new Models.ActionResult(HttpStatusCode.BadRequest, ex.Message); // In the case of argument exception we will be getting meaning full error message.
            }
            catch (Exception)
            {
                result = new Models.ActionResult(HttpStatusCode.InternalServerError, "Internal server error has accured while getting last location of the vehicle."); // no need to share implementation details with the consumer.
            }

            return result;
        }

        // POST: api/Tracking
        public async Task<Models.ActionResult> Post(Tracking tracking)
        {
            Models.ActionResult result = null;
            try
            {
                if (tracking != null && ModelState.IsValid)
                {
                    await TrackingManager.Record(tracking);
                    result = new Models.ActionResult(HttpStatusCode.OK, true);
                }
                else
                {
                    result = tracking == null ? new Models.ActionResult(HttpStatusCode.BadRequest, "Tracking data is required") : new Models.ActionResult(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (ArgumentException ex)
            {
                result = new Models.ActionResult(HttpStatusCode.BadRequest, ex.Message); // In the case of argument exception we will be getting meaning full error message.
            }
            catch (Exception)
            {
                result = new Models.ActionResult(HttpStatusCode.InternalServerError, "Internal server error has accured while recording tracking entry."); // no need to share implementation details with the consumer.
            }

            return result;
        }

        // Search: api/Tracking/Search
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Tracking/Search")]
        public async Task<Models.ActionResult> Search(TrackingSearcher searcher)
        {
            Models.ActionResult result = null;
            try
            {
                if (searcher != null && ModelState.IsValid)
                {
                   var searchResult = await TrackingManager.Search(searcher);
                    result = new Models.ActionResult(HttpStatusCode.OK, searchResult);
                }
                else
                {
                    result = searcher == null ? new Models.ActionResult(HttpStatusCode.BadRequest, "Search parameters are required") : new Models.ActionResult(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (ArgumentException ex)
            {
                result = new Models.ActionResult(HttpStatusCode.BadRequest, ex.Message); // In the case of argument exception we will be getting meaning full error message.
            }
            catch (Exception)
            {
                result = new Models.ActionResult(HttpStatusCode.InternalServerError, $"Internal server error has accured while searching tracking records for the vehicle:{searcher?.VehicleID}"); // no need to share implementation details with the consumer.
            }

            return result;
        }

       
        #region Override
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _trackingManager?.Dispose();
        }
        #endregion
    }
}
