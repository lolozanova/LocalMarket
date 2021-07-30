using LocalMarket.Services.Products;
using System;
using System.Collections.Generic;

namespace LocalMarket.Models.Product.AllProducts
{
    public class AllProductsSearchModel
    {
        public const int ProductsPerPage = 4;

        public string Keyword { get; set; }

        public int CurrPage { get; set; } = 1;

        public int TotalPages { get; set; }

        public ICollection<ProductViewModel> Products { get; set; }
    }
}
