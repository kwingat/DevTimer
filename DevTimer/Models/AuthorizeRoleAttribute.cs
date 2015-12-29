using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                var logonUrl = url.Action("Http", "Error", new { Id = 401, Area = "" });
                context.Result = new RedirectResult(logonUrl);

                // TODO: Send the user somewhere more constructive
            }
        }
    }

    public static class Role
    {
        public const string Administrator = "Administrator";
        public const string CallCenter = "Call Center";
    }
}