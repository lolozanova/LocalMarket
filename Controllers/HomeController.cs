using System.Diagnostics;
using System.Linq;
using LocalMarket.Data;
using LocalMarket.Models;
using LocalMarket.Models.Home;
using LocalMarket.Models.Product;
using LocalMarket.Services.Statistics;
using Microsoft.AspNetCore.Mvc;


namespace LocalMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticService statisticService;
        public HomeController(IStatisticService statistic)
        {
            
            statisticService = statistic;
        }

        public IActionResult Index()
        {
            var statistics = statisticService.GetStatistic();

            return View(new IndexViewModel
                                  {
                                      ProductsCount = statistics.ProductsCount,
                                      ProducersCount = statistics.ProducersCount,
                                      TownsCount = statistics.TownsCount
                                  });
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
