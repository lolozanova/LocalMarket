using LocalMarket.Services.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static LocalMarket.Data.DataConstants.Product;

namespace LocalMarket.Models.Product.AddProduct
{
    public class ProductFormModel
    {
        public int Id { get; init; }

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

        public string UnitName { get; set; }

        public IEnumerable<UnitServiceModel> Units { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Display(Name ="Category")]
        public int CategoryId { get; init; }

        public string CategoryName { get; set; }

        public IEnumerable<CategoryServiceModel> Categories { get; set; }

        public int ProducerId { get; set; }

        public bool IsApproved { get; set; } 

    }
}
