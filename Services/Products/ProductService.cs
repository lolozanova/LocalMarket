using LocalMarket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using LocalMarket.Data.Models;
using LocalMarket.Models.Product.AddProduct;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LocalMarket.Models.Product.AllProducts;

namespace LocalMarket.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly LocalMarketDbContext data;

        private readonly IMapper mapper;

        public ProductService(LocalMarketDbContext dbContext, IMapper automapper)
        {
            data = dbContext;
            mapper = automapper;
        }

        public ProductServiceModel GetProductById(int productId)
        {
            var productFromDB = data.Products
               .Where(p => p.Id == productId).FirstOrDefault();


            var productToEdit = mapper.Map<ProductServiceModel>(productFromDB);

            productToEdit.Categories = GetProductCategories();
            productToEdit.Units = GetProductUnits();

            return (productToEdit);
        }

        public AllProductsServiceSearchModel GetAllProducts(string keyword, int currPage)
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
                                   UnitName = p.Unit.Name,
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

        public IEnumerable<ProductServiceModel> GetProductsByUserId(string userId)
        {
            var products = data.Products
                              .OrderBy(p => p.Name)
                              .Where(p => p.Producer.UserId == userId)
                              .Select(p => new ProductServiceModel  
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Description = p.Description,
                                  Price = p.Price,
                                  UnitName = p.Unit.Name,
                                  ImageUrl = p.ImageUrl
                              })
                               .ToList();

            return (products);
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

        public int Create(ProductFormModel productModel, int producerId)
        {
            var product = mapper.Map<Product>(productModel);

            product.ProducerId = producerId;
           

            data.Products.Add(product);

            data.SaveChanges();

            return product.Id;
        }

        public bool Edit(int productId, ProductFormModel productModel, int producerId)
        {
            var productToEdit = data.Products.FirstOrDefault(p => p.Id == productId);

            if (productToEdit.ProducerId != producerId)
            {
                return false;
            }
          
                productToEdit.Name = productModel.Name;
                productToEdit.Description = productModel.Description;
                productToEdit.Price = productModel.Price;
                productToEdit.CategoryId = productModel.CategoryId;
                productToEdit.UnitId = productModel.UnitId;
                productToEdit.ImageUrl = productModel.ImageUrl;

            data.SaveChanges();

            return true;
        }

        public void Delete(int id)
        {
            var product = data.Products.FirstOrDefault(p => p.Id == id);

            data.Remove(product);

            data.SaveChanges();
        }
    }
}
