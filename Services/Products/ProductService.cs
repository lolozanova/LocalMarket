using LocalMarket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using LocalMarket.Data.Models;

namespace LocalMarket.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly LocalMarketDbContext data;

        public ProductService(LocalMarketDbContext dbContext)
        {
            data = dbContext;
        }

        public AllProductsServiceSearchModel GetProducts(string keyword, int currPage)
        {
            var productsAsQuaryable = data.Products.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                productsAsQuaryable = productsAsQuaryable.Where(p => p.Name.Contains(keyword.ToLower()) || p.Description.Contains(keyword.ToLower()));

            }

            var totalProducts = productsAsQuaryable.Count();

            var products = productsAsQuaryable
                               .OrderBy(p => p.Name)
                               .Skip((currPage - 1) * AllProductsServiceSearchModel.ProductsPerPage)
                               .Take(AllProductsServiceSearchModel.ProductsPerPage)
                               .Select(p => new ProductServiceModel
                               {
                                   Name = p.Name,
                                   Description = p.Description,
                                   Price = p.Price,
                                   Unit = p.Unit.Name,
                                   ImageUrl = p.ImageUrl
                               })
                                .ToList();

            var searchProducts = new AllProductsServiceSearchModel
            {
                Keyword = keyword,
                Products = products,
                TotalPages = (int)Math.Ceiling((double)totalProducts / AllProductsServiceSearchModel.ProductsPerPage),
            };

            return searchProducts;
        }

        public IEnumerable<UnitServiceModel> GetProductUnits()
        {
            var units = data.Units
                                    .Select(c => new UnitServiceModel
                                    {
                                        Id = c.Id,
                                        Name = c.Name
                                    })
                                    .ToList();

            return units;
        }

        public IEnumerable<CategoryServiceModel> GetProductCategories()
        {
            var categories = data.Categories
                                    .Select(c => new CategoryServiceModel
                                    {
                                        Id = c.Id,
                                        Name = c.Name
                                    })
                                    .ToList();

            return categories;
        }

        public bool CategoryExist(int categoryId)
        {
            return data.Categories.Any(c => c.Id == categoryId);
        }

        public bool UnitExist(int unitId)
        {
            return data.Units.Any(c => c.Id == unitId);
        }

        public int Create(string name, string description, decimal price, int unitId, string imageUrl, int categoryId, int producerId)
        {
            var product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                UnitId = unitId,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                ProducerId = producerId
            };

            data.Products.Add(product);

            return product.Id;
        }

        public bool Edit(int id, string name, string description, decimal price, int unitId, string imageUrl, int categoryId, int producerId)
        {
            var productToEdit = data.Products.FirstOrDefault(p => p.Id == id);

            if (productToEdit.ProducerId != producerId)
            {
                return false;
            }
          
                productToEdit.Name = name;
                productToEdit.Description = description;
                productToEdit.Price = price;
                productToEdit.CategoryId = categoryId;
                productToEdit.UnitId = unitId;
                productToEdit.ImageUrl = imageUrl;

            data.SaveChanges();

            return true;
        }
    }
}
