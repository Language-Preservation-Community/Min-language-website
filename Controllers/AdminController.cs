using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinLanguage.Controllers
{
    // TODO: Change to [Authorize(Roles = "Admin")] or similar
    [Authorize]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserEdit(string email)
        {
            if (email == null)
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View(model: email);
            }
            return RedirectToAction(nameof(UserRoleEdit), new { user.Id });
        }

        public async Task<IActionResult> UserRoleEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View();
            }
            return View(await GetPerRoleUserStatus(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRoleEdit(string id, IFormCollection collection)
        {
            var user = await _userManager.FindByIdAsync(id);
            foreach (var item in await GetPerRoleUserStatus(user))
            {
                string role = item.Key;
                bool userHasRole = item.Value;
                string checkBoxValue = collection[role][0];
                if (checkBoxValue == "true" && !userHasRole)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
                else if (checkBoxValue == "false" && userHasRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
            }
            return RedirectToAction(nameof(UserRoleEdit), new { id });
        }

        private async Task<Dictionary<string, bool>> GetPerRoleUserStatus(IdentityUser user)
        {
            var userRoles = (await _userManager.GetRolesAsync(user)).ToHashSet();
            return _roleManager.Roles.ToDictionary(r => r.Name, r => userRoles.Contains(r.Name));
        }
    }
}
