using LocalMarket.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService service)
        {
            productService = service;
        }
        public IActionResult Manage()
        {
           var allProducts = productService.GetAllProducts(1,null, false);

            return View(allProducts);
        }

        public IActionResult Approve(int productId)
        {
            productService.ApproveProduct(productId);

            return RedirectToAction("Manage", "Products");
        }

    }
}
