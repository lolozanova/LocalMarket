namespace LocalMarket.Models.Product.AllProducts
{
    public class ProductViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public string Unit { get; set; }

        public string ImageUrl { get; init; }

        public int CategotyId { get; init; }

    }
}
