using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace VehicleTrackingApi.Models
{
    public class ActionResult<T>
    {
        public ActionResult(HttpStatusCode status, string message) : this(status, message, default(T))
        {

        }
        public ActionResult(HttpStatusCode status, System.Web.Http.ModelBinding.ModelStateDictionary state) : this(status, null, default(T))
        {
            foreach (var item in state.Values)
            {
                Message += item.Errors.FirstOrDefault()?.ErrorMessage + Environment.NewLine;
            }
        }
        public ActionResult(HttpStatusCode status, T data) : this(status, null, data)
        {

        }
        public ActionResult(HttpStatusCode status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public T Data { set; get; }
    }
}