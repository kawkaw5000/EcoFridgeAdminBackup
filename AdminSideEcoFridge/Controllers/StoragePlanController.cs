using Microsoft.AspNetCore.Mvc;

namespace AdminSideEcoFridge.Controllers
{
    public class StoragePlanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
