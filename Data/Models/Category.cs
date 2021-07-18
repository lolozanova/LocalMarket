﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalMarket.Data.Models
{
    public class Category
    {

        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; init; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
