﻿using AutoMapper;
using LocalMarket.Data;
using LocalMarket.Data.Models;
using LocalMarket.Infrastructure;
using LocalMarket.Models.Producer;
using LocalMarket.Models.Producer.All;
using LocalMarket.Services.Producer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace LocalMarket.Controllers
{
    public class ProducerController : Controller
    {
        private readonly LocalMarketDbContext data;

        private readonly IProducerService service;

        private readonly IMapper mapper;

        public ProducerController(LocalMarketDbContext dbContext, IProducerService prodservice, IMapper automapper)
        {
            data = dbContext;
            service = prodservice;
            mapper = automapper;
        }

        [Authorize]
        public IActionResult Create()
        {

            return View(new CreateProducerFormModel { Towns = service.GetTowns() });
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

            if (!service.TownExists(producerFormModel.TownId))
            {
                ModelState.AddModelError("TownId", "Town is not valid");
            }

            if (!ModelState.IsValid)
            {
                producerFormModel.Towns = service.GetTowns();

                return View(producerFormModel);
            }

            var producer = mapper.Map<Producer>(producerFormModel);

            producer.UserId = userId;

            data.Producers.Add(producer);

            data.SaveChanges();

            return RedirectToAction("Add", "Product");
        }

        public IActionResult All()
        {
           var producers = data.Producers.Select(p => new ProducerViewModel
            {
                FullName = p.FirstName + " " + p.LastName,
                Town = p.Town.Name,
                AboutMe = p.AboutMe,

            }).ToList();

            return View(producers);
        }

       
    }
}
