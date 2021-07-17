using LocalMarket.Data;
using LocalMarket.Data.Models;
using LocalMarket.Models.Product;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LocalMarket.Controllers
{
    public class ProductController:Controller
    {
        private readonly LocalMarketDbContext data;

        public ProductController(LocalMarketDbContext dbcontext)
        {
            data = dbcontext;
        }

        public IActionResult Add()
        {
            return View(new AddProductFormModel 
           { 
            Categories = this.GetProductCategories()
            });
        }

        [HttpPost]
        public IActionResult Add(AddProductFormModel productDTO)
        {
            if (!data.Categories.Any(c => c.Id == productDTO.CategoryId))
            {
                ModelState.AddModelError("CategoryId","Category is not valid");
            }  

            if (!ModelState.IsValid)
            {
               productDTO.Categories = this.GetProductCategories();

               return View(productDTO);
            }

            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                 Price = productDTO.Price,
                ImageUrl = productDTO.ImageUrl,
                CategoryId = productDTO.CategoryId
            };

            data.Products.Add(product);

            data.SaveChanges();

            return RedirectToAction("Home");
          
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
    }
}
