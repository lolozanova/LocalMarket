using LocalMarket.Data;
using LocalMarket.Data.Models;
using LocalMarket.Infrastructure;
using LocalMarket.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LocalMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly LocalMarketDbContext data;

        public ProductController(LocalMarketDbContext dbcontext)
        {
            data = dbcontext;
        }

        [Authorize]
        public IActionResult Add()
        {

            if (!IsProducer())
            {
                return Redirect("/Producer/Create");
            }

            return View(new AddProductFormModel
            {
                Categories = this.GetProductCategories(),
                Units = this.GetProductUnits()

            }) ;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddProductFormModel productModel)
        {
            if (!IsProducer())
            {
                return Redirect("/Producer/Create");
            }

            if (!data.Categories.Any(c => c.Id == productModel.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category is not valid");
            }

            if (!data.Units.Any(c => c.Id == productModel.UnitId))
            {
                ModelState.AddModelError("UnitId", "Unit is not valid");
            }

            if (!ModelState.IsValid)
            {
                productModel.Categories = this.GetProductCategories();
                productModel.Units = this.GetProductUnits();

                return View(productModel);
            }

            var producerId = this.data
              .Producers
              .Where(p => p.UserId == this.User.GetId())
              .Select(p => p.Id)
              .FirstOrDefault();

            if (producerId == 0)
            {
                return RedirectToAction("Create", "Producer");
            }

            var product = new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                UnitId = productModel.UnitId,
                ImageUrl = productModel.ImageUrl,
                CategoryId = productModel.CategoryId,
              ProducerId = producerId
            };

            data.Products.Add(product);

            data.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
        public IActionResult All(int page=1)
        {
            var products = data.Products
                                .Select(p => new AllProductsViewModel
                                {
                                    Name = p.Name,
                                    Description = p.Description,
                                    Price = p.Price,
                                    Unit = p.Unit.Name,
                                    ImageUrl = p.ImageUrl
                                })
                                .ToList();
            return View(products);
        }
        public IActionResult Search(string keyword)
        {

            var productsAsQuaryable = data.Products.AsQueryable();
            
            if (!string.IsNullOrEmpty(keyword))
            {
                productsAsQuaryable = productsAsQuaryable.Where(p => p.Name.Contains(keyword.ToLower()) || p.Description.Contains(keyword.ToLower()));
                          
            }

           var searchedProducts = productsAsQuaryable.Select(p => new AllProductsViewModel
            {
                Name = p.Name,
                CategotyId = p.CategoryId,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price
            })
                .ToList();

            return View(searchedProducts);
        }

        private IEnumerable<CategoryViewModel> GetProductCategories()
        {
            var categories = data.Categories
                                    .Select(c => new CategoryViewModel
                                    {
                                        Id = c.Id,
                                        Name = c.Name
                                    })
                                    .ToList();

            return categories;
        }

        private IEnumerable<UnitViewModel> GetProductUnits()
        {
            var units = data.Units
                                    .Select(c => new UnitViewModel
                                    {
                                        Id = c.Id,
                                        Name = c.Name
                                    })
                                    .ToList();

            return units;
        }

        private bool IsProducer()
        {
            return data.Producers
                        .Any(p => p.UserId == this.User.GetId());
        }


    }
}
