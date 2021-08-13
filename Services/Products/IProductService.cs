using LocalMarket.Models.Product.AddProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalMarket.Services.Products
{
    public interface IProductService
    {
        public ProductServiceModel GetProductById(int productId);

        public AllProductsServiceSearchModel GetAllProducts(int currPage, string keyword, bool publicOnly);

        public IEnumerable<ProductServiceModel> GetProductsByUserId(string userId);

        public IEnumerable<UnitServiceModel> GetProductUnits();


        public IEnumerable<CategoryServiceModel> GetProductCategories();

        public void ApproveProduct(int productId);

        public bool CategoryExist(int categoryId);

        public bool UnitExist(int unitId);

        public int Create(ProductFormModel productModel,
              int producerId);

        public bool Edit(int productId,ProductFormModel productModel, int producerId);

        public void Delete(int id);

    }
}
