using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocalMarket.Data.Models
{
    public class Town
    {
        public Town()
        {
            Producers = new HashSet<Producer>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Producer> Producers { get; set; }
    }
}
