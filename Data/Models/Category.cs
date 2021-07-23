using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static LocalMarket.Data.DataConstants.Common;

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
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
