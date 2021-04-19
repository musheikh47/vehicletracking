using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace VehicleTrackingApi.Models
{
    public class ActionResult
    {
        public ActionResult(HttpStatusCode status,string message) : this(status,message,null)
        {

        }
        public ActionResult(HttpStatusCode status, System.Web.Http.ModelBinding.ModelStateDictionary state) : this(status, null, null)
        {
            foreach (var item in state.Values)
            {
                Message += item.Errors.FirstOrDefault()?.ErrorMessage + Environment.NewLine;
            }
        }
        public ActionResult(HttpStatusCode status, object data) : this(status,null,data)
        {

        }
        public ActionResult(HttpStatusCode status,string message,  object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public object Data { set; get; }
    }
}