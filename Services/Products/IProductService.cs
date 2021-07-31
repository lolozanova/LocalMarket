using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalMarket.Services.Products
{
    public interface IProductService
    {

        public AllProductsServiceSearchModel GetProducts(string keyword, int currPage);

        public IEnumerable<UnitServiceModel> GetProductUnits();


        public IEnumerable<CategoryServiceModel> GetProductCategories();

        public bool CategoryExist(int categoryId);

        public bool UnitExist(int unitId);

        public int Create(string name, 
                string description,
                decimal price,
                int unitId,
                string imageUrl,
                int categoryId,
              int producerId);

        public bool Edit(int id,
               string name,
               string description,
               decimal price,
               int unitId,
               string imageUrl,
               int categoryId,
               int producerId
             );

        public void Delete(int id);

    }
}
