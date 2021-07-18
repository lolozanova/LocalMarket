namespace LocalMarket.Models.Product
{
    public class AllProductsViewModel
    {
        public const int ProductPerPage = 8;
        public string Name { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public string Unit { get; set; }

        public string ImageUrl { get; init; }

        public int CategotyId { get; init; }

        public int CurrentPage { get; set; }
    }
}
