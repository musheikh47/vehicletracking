using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;
using VehicleTracking.Common.DTO;
using VehicleTracking.Common.Interfaces;
using VehicleTracking.Engine;

namespace VehicleTrackingApi.Controllers
{
    public class AccountController : ApiController
    {
        private AccountManager _accountManager;
        public AccountManager AccountManager
        {
            get
            {
                if (_accountManager == null)
                {
                    _accountManager = new AccountManager(Models.UnityHelper.Container.Resolve<IUserRepository>());
                }
                return _accountManager;
            }
        }
        // POST: api/Account
        // For security purpose we should require this application to run on SSL. I am not enforcing SSL to make the deployment simple for the demo purpose.
        public async Task<Models.ActionResult<string>> Post(User user)
        {
            Models.ActionResult<string> result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedInUser = await AccountManager.Authenticate(user);
                    if (loggedInUser != null)
                    {
                        var token = Models.JWTHelper.GenerateJWTToken(ConfigurationManager.AppSettings["jwt_issuer"], ConfigurationManager.AppSettings["jwt_secret"], user.ID.ToString(),user.Username,user.Role);
                        result = new Models.ActionResult<string>(HttpStatusCode.OK, data: token);
                    }
                    else
                    {
                        result = new Models.ActionResult<string>(HttpStatusCode.NotFound, "Invalid username or password");
                    }                  
                }
                else
                {
                    result = new Models.ActionResult<string>(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (ArgumentException ex)
            {
                result = new Models.ActionResult<string>(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                result = new Models.ActionResult<string>(HttpStatusCode.InternalServerError, $"Internal server error has accured authenticating user {user.Username}.");
            }

            return result;
        }

        #region Override
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _accountManager?.Dispose();
        }
        #endregion
    }
}
