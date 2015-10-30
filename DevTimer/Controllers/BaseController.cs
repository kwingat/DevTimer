using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevTimer.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Request.Cookies.AllKeys.Contains("timezoneoffset"))
            {
                HttpCookie httpCookie = HttpContext.Request.Cookies["timezoneoffset"];

                if (httpCookie != null)
                    Session["timezoneoffset"] = httpCookie.Value;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}