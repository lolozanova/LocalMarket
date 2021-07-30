using LocalMarket.Data;
using LocalMarket.Data.Models;
using LocalMarket.Infrastructure;
using LocalMarket.Models.Product.AddProduct;
using LocalMarket.Models.Product.AllProducts;
using LocalMarket.Services.Producer;
using LocalMarket.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LocalMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly LocalMarketDbContext data;

        private readonly IProducerService producerService;

        private readonly IProductService productService;

        public ProductController(LocalMarketDbContext dbcontext, IProducerService _producerService, IProductService _productService)
        {
            data = dbcontext;
            producerService = _producerService;
            productService = _productService;
        }

        [Authorize]
        public IActionResult Add()
        {

            if (!producerService.IsProducer(User.GetId()))
            {
                return Redirect("/Producer/Create");
            }

            return View(new ProductFormModel
            {
                Categories = productService.GetProductCategories(),
                Units = productService.GetProductUnits()

            }); ;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ProductFormModel productModel)
        {
            if (!producerService.IsProducer(User.GetId()))
            {
                return Redirect("/Producer/Create");
            }

            if (!productService.CategoryExist(productModel.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category is not valid");
            }

            if (!productService.UnitExist(productModel.UnitId))
            {
                ModelState.AddModelError("UnitId", "Unit is not valid");
            }

            if (!ModelState.IsValid)
            {
                productModel.Categories = productService.GetProductCategories();
                productModel.Units = productService.GetProductUnits();
                return View(productModel);
            }

            var producerId = producerService.GetProducerById(User.GetId());

            if (producerId == 0)
            {
                return RedirectToAction("Create", "Producer");
            }

                productService.Create(productModel.Name,
                productModel.Description,
                productModel.Price,
                productModel.UnitId,
                productModel.ImageUrl,
                productModel.CategoryId,
                producerId
                );

            return RedirectToAction("Index", "Home");

        }

        [Authorize]
        public IActionResult Edit(int productId)
        {
            if (!producerService.IsProducer(User.GetId()))
            {
                return Redirect("/Producer/Create");
            }

           
            var product = data.Products
                .Where(p => p.Id == productId)
                .Select(p => new ProductFormModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    UnitId = p.UnitId,
                    UnitName = p.Unit.Name,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    ProducerId = p.ProducerId
                })
                .FirstOrDefault();

            product.Categories = productService.GetProductCategories();
            product.Units = productService.GetProductUnits();

            return View(product);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int productId, ProductFormModel productModel)
        {
            if (!producerService.IsProducer(User.GetId()))
            {
                return Redirect("/Producer/Create");
            }
           
            if (!productService.CategoryExist(productModel.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category is not valid");
            }

            if (!productService.UnitExist(productModel.UnitId))
            {
                ModelState.AddModelError("UnitId", "Unit is not valid");
            }

            if (!ModelState.IsValid)
            {
                productModel.Categories = productService.GetProductCategories();
                productModel.Units = productService.GetProductUnits();
                return View(productModel);
            }

         int producerId = producerService.GetProducerById(User.GetId());

            var successfulEdited = productService.Edit(productId, productModel.Name,productModel.Description,productModel.Price, productModel.UnitId, productModel.ImageUrl, productModel.CategoryId, producerId );

            if (!successfulEdited)
            {
                return BadRequest();
            }

            return RedirectToAction("Mine", "Product", new { userId=User.GetId()});
        }

        public IActionResult All(AllProductsServiceSearchModel searchModel)
        {
            var productsAsQuaryable = data.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.Keyword))
            {
                productsAsQuaryable = productsAsQuaryable.Where(p => p.Name.Contains(searchModel.Keyword.ToLower()) || p.Description.Contains(searchModel.Keyword.ToLower()));

            }
            var totalProducts = productsAsQuaryable.Count();

            var products = productsAsQuaryable
                               .OrderBy(p => p.Name)
                               .Skip((searchModel.CurrPage - 1) * AllProductsSearchModel.ProductsPerPage)
                               .Take(AllProductsSearchModel.ProductsPerPage)
                               .Select(p => new ProductViewModel
                               {
                                   Name = p.Name,
                                   Description = p.Description,
                                   Price = p.Price,
                                   Unit = p.Unit.Name,
                                   ImageUrl = p.ImageUrl
                               })
                                .ToList();

            var searchProducts = new AllProductsSearchModel
            {
                Keyword = searchModel.Keyword,
                Products = products,
                TotalPages = (int)Math.Ceiling((double)totalProducts / Services.Products.AllProductsServiceSearchModel.ProductsPerPage)

            };
            return View(searchProducts);
        }

        [Authorize]
        public IActionResult Mine(string userId)
        {
            var products = data.Products
                              .OrderBy(p => p.Name)
                              .Where(p=>p.Producer.UserId == userId)
                              .Select(p => new ProductViewModel
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Description = p.Description,
                                  Price = p.Price,
                                  Unit = p.Unit.Name,
                                  ImageUrl = p.ImageUrl
                              })
                               .ToList();

            return View(products);
        }

       

     

      


    }
}
