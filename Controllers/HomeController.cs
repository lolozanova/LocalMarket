using System.Diagnostics;
using System.Linq;
using LocalMarket.Data;
using LocalMarket.Models;
using LocalMarket.Models.Home;
using LocalMarket.Models.Product;
using Microsoft.AspNetCore.Mvc;


namespace LocalMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly LocalMarketDbContext data;
        public HomeController(LocalMarketDbContext dbContext)
        {
            data = dbContext;
        }

        public IActionResult Index()
        {
            var indexModel = new IndexViewModel 
            {
                ProductsCount = data.Products.Count() 
            };
            return View(indexModel);
        }

        public IActionResult AboutUs()
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
