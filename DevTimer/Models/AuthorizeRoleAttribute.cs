 using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 using System.Web.UI.WebControls;
 using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;

namespace DevTimer.Models
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        //private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private string[] UserProfilesRequired { get; set; }

        public AuthorizeRolesAttribute(params string[] userProfilesRequired)
        {
            UserProfilesRequired = userProfilesRequired;
        }

        public override void OnAuthorization(AuthorizationContext context)
        {
            bool authorized = UserProfilesRequired.Any(role => HttpContext.Current.User.IsInRole(role));

            if (!authorized)
            {
                var url = new UrlHelper(context.RequestContext);
                var errorUrl = url.Action("Error", "Home", new { Id = 401, Area = "" });
                context.Result = new RedirectResult(errorUrl);

                // TODO: Send the user somewhere more constructive
            }
        }
    }

    public static class Role
    {
        public const string Administrator = "Administrator";

        public const string CallCenter = "Dept - Call Center";
        public const string Programmers = "Dept - Programmers";

        public const string Hourly = "Time-Hourly";
        public const string Salary = "Time-Salary";
    }
}