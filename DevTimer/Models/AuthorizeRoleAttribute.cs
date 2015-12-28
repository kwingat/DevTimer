using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DevTimer.Models
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public AuthorizeRolesAttribute(params string[] roles)
            : base()
        {
            Roles = string.Join(",", roles);
        }

        public override void OnAuthorization(AuthorizationContext context)
        {
            bool authorized = false;
            var roles = context.ActionDescriptor.UniqueId;
            var x = _context.Roles.ToList();

            //foreach (var role in roles)
            //{
            //    if (HttpContext.Current.User.IsInRole(role.ToString()))
            //    {
            //        authorized = true;
            //        break;
            //    }
            //}

            if (!authorized)
            {
                var url = new UrlHelper(context.RequestContext);
                var logonUrl = url.Action("Http", "Error", new { Id = 401, Area = "" });
                context.Result = new RedirectResult(logonUrl);

                return;
            }
        }
    }


    public static class Role
    {
        public const string Administrator = "Administrator";
        public const string CallCenter = "Call Center";
    }
}