using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalMarket.Services.Statistics
{
    public interface IStatisticService
    {
        StatisticsServiceModel GetStatistic();
    }
}
