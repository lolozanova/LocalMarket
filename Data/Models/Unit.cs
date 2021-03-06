
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static LocalMarket.Data.DataConstants.Common;
namespace LocalMarket.Data.Models
{
    public class Unit
    {
        public Unit()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }

    }
}
