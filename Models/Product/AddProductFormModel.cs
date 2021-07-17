using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Sdk;
using static LocalMarket.Data.DataConstants;

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

        [Required]
        [Url]
        public string ImageUrl { get; init; }

        public int CategoryId { get; init; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

    }
}
