using System.Diagnostics;
using LocalMarket.Models;
using Microsoft.AspNetCore.Mvc;


namespace LocalMarket.Controllers
{
    public class HomeController : Controller
    {
  
        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult Products()
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
