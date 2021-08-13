using LocalMarket.Data;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalMarket.Services.Statistics
{
    public class StatisticService : IStatisticService
    {
        private readonly LocalMarketDbContext data;
        private readonly IMemoryCache cache;
        public StatisticService(LocalMarketDbContext dbContext, IMemoryCache memoryCache)
        {
            data = dbContext;
            cache = memoryCache;
        }

        StatisticsServiceModel IStatisticService.GetStatistic()
        {
            const string cacheKey = "homePageStatistic";

            var statistics = cache.Get<StatisticsServiceModel>(cacheKey);

            if (statistics == null)
            {
                statistics = new StatisticsServiceModel
                {
                    ProducersCount = data.Producers.Count(),
                    ProductsCount = data.Products.Count(),
                    TownsCount = data.Towns.Count()
                };

                cache.Set(cacheKey, statistics, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(15)));
            }
             
            return statistics;
        }
    }
}
