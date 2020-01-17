using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Napredne_Aplikacija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Napredne_Aplikacija.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministratorController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ExtendedKorisnik> userManager;

        public AdministratorController(RoleManager<IdentityRole> roleManager, UserManager<ExtendedKorisnik> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult createRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> createRole(RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administrator");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult listRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(String id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rola sa id = {id} ne postoji! ";
                return RedirectToAction("ListRoles", "Administrator");
            }
            var model = new EditRoleModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rola sa id = {model.Id} ne postoji! ";
                return RedirectToAction("ListRoles", "Administrator");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administrator");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        public async Task<IActionResult> EditUserInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rola sa id={roleId} ne postoji!";
                return View("ListRoles", "Administrator");
            }

            var model = new List<UserRoleModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleModel = new UserRoleModel
                {
                    UserID = user.Id,
                    UserName = user.UserName,

                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleModel.IsSelected = true;
                }
                else
                {
                    userRoleModel.IsSelected = false;
                }
                model.Add(userRoleModel);

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rola sa id={roleId} ne postoji!";
                return View("ListRoles", "Administrator");
            }

            for(int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserID);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user,role.Name)))
                {
                   result = await userManager.AddToRoleAsync(user,role.Name);
                }
                else if(!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1)) continue;
                    else return RedirectToAction("EditRole", new { id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { id = roleId });
        }
    }
}
