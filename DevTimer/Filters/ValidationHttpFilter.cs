using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using Newtonsoft.Json.Linq;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace DevTimer.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ValidationHttpFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                var errors = new JObject();
                foreach (var key in modelState.Keys)
                {
                    var state = modelState[key];
                    if (state.Errors.Any())
                        errors[key] = state.Errors.First().ErrorMessage;
                }

                actionContext.Response = actionContext.Request.CreateResponse<JObject>(HttpStatusCode.BadRequest, errors);
            }
        }
    }
}