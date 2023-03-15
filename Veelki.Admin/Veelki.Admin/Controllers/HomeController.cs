using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Veelki.Admin.Models;
using Veelki.Data.Entities;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Veelki.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<Users> _userManager;

        public HomeController(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            var user = Request.Cookies["loginUserDetail"]!=null? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]):null; 
            if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
