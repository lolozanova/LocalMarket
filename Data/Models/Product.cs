using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LocalMarket.Data.DataConstants.Car;

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

        [Column(TypeName = "decimal(18, 2)")]
        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        public int UnitId { get; set; }

        public Unit Unit { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public int ProducerId { get; set; }

        public Producer Producer { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

    }
}
