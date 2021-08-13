using System.Collections.Generic;

namespace LocalMarket.Services.Products
{
    public class ProductServiceModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public int UnitId { get; init; }

        public string UnitName { get; set; }

        public IEnumerable<UnitServiceModel> Units { get; set; }

        public string ImageUrl { get; init; }

        public int CategoryId { get; init; }

        public string CategoryName { get; init; }

        public IEnumerable<CategoryServiceModel> Categories { get; set; }

        public int ProducerId { get; init; }

        public string ProducerName { get; set; }

        public bool IsApproved { get; set; }
    }
}
