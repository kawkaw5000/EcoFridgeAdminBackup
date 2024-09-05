using AdminSideEcoFridge.Contracts;
using AdminSideEcoFridge.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AdminSideEcoFridge.Repository;
using AdminSideEcoFridge.Utils;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AdminSideEcoFridge.Controllers
{

    [Authorize(Policy = "AdminPolicy")]
    public class HomeController : BaseController
    {      

        public IActionResult Dashboard()
        {
            List<VwUsersRoleView> userList = _vwUsersRoleViewRepo.GetAll();
            List<VwUsersFoodItem> foodList = _vwUsersFoodItemRepo.GetAll();
            List<User> user = _userRepo.GetAll();

            var viewModel = new DashboardViewModel
            {
                UserList = userList,
                FoodList = foodList,
                User = user
            };

            return View(viewModel);
        }

        public IActionResult GetFoodItemsByUserId(int userId)
        {
            List<VwUsersFoodItem> foodItems = _vwUsersFoodItemRepo.GetAll()
                                               .Where(f => f.UserId == userId)
                                               .ToList();
            return Json(foodItems);
        }

        public IActionResult GetUserById(int id)
        {
            var user = _userRepo.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return Json(user);
        }
  
        #region Admin Create
        public IActionResult AdminCreate()
        {
            return View();
        }

        public IActionResult RegularCreate()
        {
            return View();
        }

        public IActionResult FoodBusinessCreate()
        {
            return View();
        }

        public IActionResult OrganizationCreate()
        {
            return View();
        }

        #endregion

    }
}
