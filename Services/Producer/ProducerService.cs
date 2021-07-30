using LocalMarket.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LocalMarket.Services.Producer
{
    public class ProducerService : IProducerService
    {
        private readonly LocalMarketDbContext data;

        public ProducerService(LocalMarketDbContext dbContext)
        {
             data= dbContext;
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
    }


}
