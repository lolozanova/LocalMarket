using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalMarket.Services.Products
{
    public class AllProductsServiceSearchModel
    {

        public const int ProductsPerPage = 4;

        public string Keyword { get; set; }
       
        public int CurrPage { get; set; } = 1;

        public int TotalPages { get; set; }

        public ICollection<ProductServiceModel> Products { get; set; }

    }
}
