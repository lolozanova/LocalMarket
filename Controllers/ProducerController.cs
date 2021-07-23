using LocalMarket.Data;
using LocalMarket.Data.Models;
using LocalMarket.Infrastructure;
using LocalMarket.Models.Producer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace LocalMarket.Controllers
{
    public class ProducerController:Controller
    {
        private readonly LocalMarketDbContext data;

        public ProducerController(LocalMarketDbContext dbContext)
        {
            data = dbContext;
        }

        [Authorize]
        public IActionResult Create()
        {
           
            return View(new CreateProducerFormModel {Towns = GetTowns() });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateProducerFormModel producerFormModel)
        {

           var userId = User.GetId();

           var userIsProducer = data.Producers
                                    .Any(p => p.UserId == userId);

            if (userIsProducer)
            {
                return BadRequest();
            }

            if (!data.Towns.Any(c => c.Id == producerFormModel.TownId))
            {
                ModelState.AddModelError("TownId", "Town is not valid");
            }

            if (!ModelState.IsValid)
            {
                producerFormModel.Towns = this.GetTowns();

                return View(producerFormModel);
            }

            var producer = new Producer
            {
                FirstName = producerFormModel.FirstName,
                LastName = producerFormModel.LastName,
                PhoneNumber = producerFormModel.PhoneNumber,
                UserId = userId,
                TownId = producerFormModel.TownId,
               
            };

            data.Producers.Add(producer);

            data.SaveChanges();

            return RedirectToAction("Add", "Product");
        }

        private IEnumerable<TownViewModel> GetTowns()
        {
            var towns = data.Towns
                                    .Select(t => new TownViewModel
                                    {
                                        Id = t.Id,
                                        Name = t.Name
                                    })
                                    .ToList();

            return towns;
        }
    }
}
