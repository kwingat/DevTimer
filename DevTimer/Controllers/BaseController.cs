using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevTimer.Models;
using Microsoft.AspNet.Identity.Owin;

namespace DevTimer.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationUserManager _userManager;
        

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

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }
        }
    }
}