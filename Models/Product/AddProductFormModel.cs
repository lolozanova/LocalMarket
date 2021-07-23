using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static LocalMarket.Data.DataConstants.Product;

namespace LocalMarket.Models.Product
{
    public class AddProductFormModel
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "The field description must be between {2} and {1}")]

        public string Description { get; init; }

        [Range(typeof(decimal), PriceMinValue, PriceMaxValue, ErrorMessage="The field price cannot be null")]
        public decimal Price { get; init; }

        [Display(Name = "Unit")]

        public int UnitId { get; set; }

        public IEnumerable<UnitViewModel> Units { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Display(Name ="Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

    }
}
