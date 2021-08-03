using System.Diagnostics;
using System.Linq;
using AutoMapper;
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

        private readonly IMapper mapper;

        public HomeController(IStatisticService statistic, IMapper automapper)
        {

            statisticService = statistic;
            mapper = automapper;
        }

        public IActionResult Index()
        {
            var statistics = statisticService.GetStatistic();

            var indexView = mapper.Map<IndexViewModel>(statistics);

            return View(indexView);
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
