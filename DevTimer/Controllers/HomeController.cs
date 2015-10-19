using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace DevTimer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly string _currentUserId;

        public HomeController()
        {
            try
            {
                _currentUserId = User.Identity.GetUserId();
            }
            catch (Exception)
            {

            }
            
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}