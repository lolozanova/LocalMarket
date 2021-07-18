using LocalMarket.Models.Product;
using System.Collections.Generic;

namespace LocalMarket.Models.Home
{
    public class AllProductsSearchModel
    {
        public string Keyword { get; set; }

        public IEnumerable<AllProductsViewModel> Products { get; set; }
    }
}
