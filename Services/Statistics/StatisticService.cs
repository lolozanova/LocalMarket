using LocalMarket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalMarket.Services.Statistics
{
    public class StatisticService : IStatisticService
    {
        private readonly LocalMarketDbContext data;

        public StatisticService(LocalMarketDbContext dbContext)
        {
            data = dbContext;
        }

        StatisticsServiceModel IStatisticService.GetStatistic()
        {
            var statistics = new StatisticsServiceModel
            {
                ProducersCount = data.Producers.Count(),
                ProductsCount = data.Products.Count(),
                TownsCount = data.Towns.Count()
            };

            return statistics;
        }
    }
}
