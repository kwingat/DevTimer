using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevTimer.Core;
using DevTimer.Infrastructure.Alerts;
using DevTimer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DevTimer.Controllers
{
    [AuthorizeRoles(Role.Administrator)]
    public class RolesController : BaseController
    {
        readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Roles
        public async Task<ActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();

            return View(roles);
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                _context.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"]
                });

                await _context.SaveChangesAsync();

                return RedirectToAction("Index").WithSuccess("Role created successfully!");
            }
            catch
            {
                return View(collection);
            }
        }
        
        // GET: /Roles/Edit/5
        public async Task<ActionResult> Edit(string roleName)
        {
            var thisRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));

            return View(thisRole);
        } 

        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(IdentityRole role)
        {
            try
            {
                _context.Entry(role).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToAction("Index").WithSuccess("Role saved successfully!");
            }
            catch
            {
                return View(role);
            }
        }

        // GET: /Roles/Delete/5
        public async Task<ActionResult> Delete(string roleName)
        {
            var thisRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));

            _context.Roles.Remove(thisRole);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index").WithSuccess("Role deleted successfully");
        }

        public ActionResult ManageUserRoles()
        {
            var list = _context.Roles.OrderBy(r => r.Name)
                .ToList()
                .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name})
                .ToList();

            ViewBag.Roles = list;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RoleAddToUser(string userName, string roleName)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            await UserManager.AddToRolesAsync(user.Id, roleName);

            var list = _context.Roles.OrderBy(r => r.Name)
                .ToList()
                .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name})
                .ToList();

            ViewBag.Roles = list;

            return View("ManageUserRoles").WithSuccess("Role created successfully");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetRoles(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
                //var account = new AccountController();

                ViewBag.RolesForThisUser = await UserManager.GetRolesAsync(user.Id);

                var list = _context.Roles.OrderBy(r => r.Name)
                    .ToList()
                    .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name})
                    .ToList();

                ViewBag.Roles = list;
                //ViewBag.RolesForThisUser = list;
            }

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRoleForUser(string userName, string roleName)
        {
            var message = "";
            
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));

            if (await UserManager.IsInRoleAsync(user.Id, roleName))
            {
                await UserManager.RemoveFromRoleAsync(user.Id, roleName);
                message = "Role removed from this user successfully!";
            }
            else
            {
                message = "This user doesn't belong to selected role.";
            }

            var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name});
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        } 
    }
} 