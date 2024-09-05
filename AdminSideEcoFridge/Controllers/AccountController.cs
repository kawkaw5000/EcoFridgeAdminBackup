using AdminSideEcoFridge.Models;
using AdminSideEcoFridge.Models.CustomModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AdminSideEcoFridge.Utils;

namespace AdminSideEcoFridge.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(CustomUserModelForLogIn user)
        {
            var userObj = _db.Users.Where(model => (model.Email == user.Email || model.Email == user.Email)).FirstOrDefault();

            if (userObj == null || userObj.EmailConfirmed == false)
            {
                return View();
            }

            if (user.Password != userObj.Password)
            {
                return View();
            }

            var userRoleVw = _db.VwUsersRoleViews.Where(m => m.UserId == userObj.UserId).FirstOrDefault();

            if (userRoleVw == null || String.IsNullOrEmpty(userRoleVw.RoleName))
            {
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, Convert.ToString(userObj.UserId)),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRoleVw.RoleName)

            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

            return RedirectToAction("Dashboard", "Home");
         }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        #region EditSection -
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userRepo.Get(id);            

            if (user == null)
            {
                return NotFound();
            }

            return PartialView("_EditUserPartialView", user);
        }

        [HttpPost]
        public IActionResult Edit(User u)
        {
      
            if (u == null)
            {
                return RedirectToAction("Login");
            }

            var result = _userRepo.Update(u.UserId, u);

            if (result == ErrorCode.Success)
            {
                TempData["Msg"] = $"User {u.Username} Updated";
                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError("", "Unable to update user. Please try again.");
            return View(u); 
        }

        [HttpGet]
        public IActionResult EditOrg(int id)
        {
            var user = _userRepo.Get(id);

            if (user == null)
            {
                return NotFound();
            }


            return PartialView("_EditOrgPartialView", user);
        }

        [HttpPost]
        public IActionResult EditOrg(User u)
        {

            if (u == null)
            {
                return RedirectToAction("Login");
            }

            var result = _userRepo.Update(u.UserId, u);

            if (result == ErrorCode.Success)
            {
                TempData["Msg"] = $"User {u.Username} Updated";
                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError("", "Unable to update user. Please try again.");
            return View(u);
        }

        [HttpGet]
        public IActionResult EditFoodResto(int id)
        {
            var user = _userRepo.Get(id);

            if (user == null)
            {
                return NotFound();
            }


            return PartialView("_EditFoodOrgPartialView", user);
        }

        [HttpPost]
        public IActionResult EditFoodResto(User u)
        {

            if (u == null)
            {
                return RedirectToAction("Login");
            }

            var result = _userRepo.Update(u.UserId, u);

            if (result == ErrorCode.Success)
            {
                TempData["Msg"] = $"User {u.Username} Updated";
                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError("", "Unable to update user. Please try again.");
            return View(u);
        }
        #endregion


    }
}
