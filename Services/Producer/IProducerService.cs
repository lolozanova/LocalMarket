﻿using LocalMarket.Models.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalMarket.Services.Producer
{
    public interface IProducerService
    {

        public bool IsProducer(string userId);

        public int GetProducerById(string userId);

        public bool TownExists(int townId);

        public IEnumerable<TownViewModel> GetTowns();

    }
}
