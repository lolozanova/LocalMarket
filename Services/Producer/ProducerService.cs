using AutoMapper;
using AutoMapper.QueryableExtensions;
using LocalMarket.Data;
using LocalMarket.Models.Producer;
using System.Collections.Generic;
using System.Linq;

namespace LocalMarket.Services.Producer
{
    public class ProducerService : IProducerService
    {
        private readonly LocalMarketDbContext data;

        private readonly IMapper mapper;

        public ProducerService(LocalMarketDbContext dbContext, IMapper automapper)
        {
            data = dbContext;
            mapper = automapper;
        }
        public bool IsProducer(string userId)
        {
            return data.Producers.Any(p => p.UserId == userId);
        }

        public int GetProducerById(string userId)
        {
            var producerId = this.data
                         .Producers
                         .Where(p => p.UserId == userId)
                         .Select(p => p.Id)
                         .FirstOrDefault();

            return producerId;
        }

        public bool TownExists(int townId)
        {
            return data.Towns.Any(c => c.Id == townId);
        }

        public IEnumerable<TownViewModel> GetTowns()
        {
            var towns = data.Towns
                                  .OrderBy(t => t.Name)
                                  .ProjectTo<TownViewModel>(mapper.ConfigurationProvider)
                                  .ToList();

            return towns;
        }
    }


}
