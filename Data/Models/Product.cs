using System.ComponentModel.DataAnnotations;
using static LocalMarket.Data.DataConstants;

namespace LocalMarket.Data.Models
{
    public class Product
    {

        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }


        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

    }
}
