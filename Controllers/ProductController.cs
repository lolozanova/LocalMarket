using AutoMapper;
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
using System.Collections.Generic;
using System.Linq;

namespace LocalMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly LocalMarketDbContext data;

        private readonly IProducerService producerService;

        private readonly IProductService productService;

        private readonly IMapper mapper;

        public ProductController(LocalMarketDbContext dbcontext, IProducerService _producerService, IProductService _productService, IMapper automapper)
        {
            data = dbcontext;
            producerService = _producerService;
            productService = _productService;
            mapper = automapper;
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

            var producerId = producerService.GetProducerIdByUserId(User.GetId());

            if (producerId == 0)
            {
                return RedirectToAction("Create", "Producer");
            }


            productService.Create(productModel, producerId);

            TempData["GlobalMessage"] = "I have successfully added a product";

            return RedirectToAction("Mine", "Product", new { userId = User.GetId() });

        }

        [Authorize]
        public IActionResult Edit(int productId)
        {
            if (!producerService.IsProducer(User.GetId()) && !User.IsInRole("Admin"))
            {
                return Redirect("/Producer/Create");
            }

            var product = productService.GetProductById(productId);
            var productModel = mapper.Map<ProductFormModel>(product);

            return View(productModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit([FromQuery] int productId, [FromForm] ProductFormModel productModel)
        {
            if (!producerService.IsProducer(User.GetId()) && User.IsInRole("Admin"))
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

            int producerId = producerService.GetProducerIdByUserId(User.GetId());

            var successfulEdited = productService.Edit(productId, productModel, producerId);

            if (!successfulEdited)
            {
                return BadRequest();
            }
            TempData["GlobalMessage"] = "You have successfully edited a product";

            return RedirectToAction("Mine", "Product", new { userId = User.GetId()});
        }

        [Authorize]

        public IActionResult Delete(int productId)
        {
            if (!producerService.IsProducer(User.GetId()) && !User.IsInRole("Admin"))
            {
                return Redirect("/Producer/Create");
            }

            productService.Delete(productId);

            TempData["GlobalMessage"] = "You have successfully deleted the product";

            return RedirectToAction("Mine", "Product", new { userId = User.GetId() });
        }

        public IActionResult All(AllProductsServiceSearchModel searchModel)
        {
            var productsAsQuaryable = data.Products.Where(p=>p.IsApproved == true);

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
                                   Id = p.Id,
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
                TotalPages = (int)Math.Ceiling((double)totalProducts / AllProductsServiceSearchModel.ProductsPerPage)

            };
            return View(searchProducts);
        }

        [Authorize]
        public IActionResult Mine(string userId)
        {
            var products = productService.GetProductsByUserId(userId);

            var productsView = mapper.Map<IEnumerable<ProductViewModel>>(products);

            return View(productsView);
        }

        public IActionResult Details(int productId)
        {
           var product = productService.GetProductById(productId);

            return View(product);
        }
    }
}
