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
using System.Security.Claims;
using Unity;
using VehicleTracking.Common.Interfaces;

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
                    _trackingManager = new TrackingManager(Models.UnityHelper.Container.Resolve<ITrackingRepository>());
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
        [System.Web.Http.Authorize(Roles = "administrator")]
        public async Task<Models.ActionResult<Tracking>> Get(int id)
        {
            Models.ActionResult<Tracking> result = null;
            try
            {
                if (id > 0)
                {
                    var trackingEntry = await TrackingManager.GetLastTrackingRecord(id);
                    result = new Models.ActionResult<Tracking>(HttpStatusCode.OK, trackingEntry);
                }
                else
                {
                    result = new Models.ActionResult<Tracking>(HttpStatusCode.BadRequest,"id should be greater than zero.");
                }
            }
            catch (ArgumentException ex)
            {
                result = new Models.ActionResult<Tracking>(HttpStatusCode.BadRequest, ex.Message); // In the case of argument exception we will be getting meaning full error message.
            }
            catch (Exception)
            {
                result = new Models.ActionResult<Tracking>(HttpStatusCode.InternalServerError, "Internal server error has accured while getting last location of the vehicle."); // no need to share implementation details with the consumer.
            }

            return result;
        }

        // POST: api/Tracking
        [System.Web.Http.Authorize(Roles = "vehicle")]
        public async Task<Models.ActionResult<bool>> Post(Tracking tracking)
        {
            Models.ActionResult<bool> result = null;
            try
            {              
                if (tracking != null && ModelState.IsValid)
                {
                    //JWT sub claim will be translated into NameIdentifier
                    var claim = ClaimsPrincipal.Current.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase));
                    if (claim == null || !int.TryParse(claim.Value, out int vid) || vid != tracking.VehicleID)
                    {
                        result = new Models.ActionResult<bool>(HttpStatusCode.Unauthorized, "You are not authorized to update the position of requested vehicle");
                    }
                    else
                    {
                        await TrackingManager.Record(tracking);
                        result = new Models.ActionResult<bool>(HttpStatusCode.OK, true);
                    }

                }
                else
                {
                    result = tracking == null ? new Models.ActionResult<bool>(HttpStatusCode.BadRequest, "Tracking data is required") : new Models.ActionResult<bool>(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (ArgumentException ex)
            {
                result = new Models.ActionResult<bool>(HttpStatusCode.BadRequest, ex.Message); // In the case of argument exception we will be getting meaning full error message.
            }
            catch (Exception)
            {
                result = new Models.ActionResult<bool>(HttpStatusCode.InternalServerError, "Internal server error has accured while recording tracking entry."); // no need to share implementation details with the consumer.
            }

            return result;
        }

        // Search: api/Tracking/Search
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Tracking/Search")]
        [System.Web.Http.Authorize(Roles = "administrator")]
        public async Task<Models.ActionResult<SearchResult>> Search(TrackingSearcher searcher)
        {
            Models.ActionResult<SearchResult> result = null;
            try
            {
                if (searcher != null && ModelState.IsValid)
                {
                   var searchResult = await TrackingManager.Search(searcher);
                    result = new Models.ActionResult<SearchResult>(HttpStatusCode.OK, searchResult);
                }
                else
                {
                    result = searcher == null ? new Models.ActionResult<SearchResult>(HttpStatusCode.BadRequest, "Search parameters are required") : new Models.ActionResult<SearchResult>(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (ArgumentException ex)
            {
                result = new Models.ActionResult<SearchResult>(HttpStatusCode.BadRequest, ex.Message); // In the case of argument exception we will be getting meaning full error message.
            }
            catch (Exception)
            {
                result = new Models.ActionResult<SearchResult>(HttpStatusCode.InternalServerError, $"Internal server error has accured while searching tracking records for the vehicle:{searcher?.VehicleID}"); // no need to share implementation details with the consumer.
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
