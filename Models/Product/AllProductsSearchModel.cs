using System;
using System.Collections.Generic;

namespace LocalMarket.Models.Product
{
    public class AllProductsSearchModel
    {
        public const int ProductsPerPage = 2;

        public string Keyword { get; set; }

        public  ICollection<AllProductsViewModel> Products { get; set; }

        public int CurrPage { get; set; } = 1;

        public int TotalPages { get; set; }
    }
}
