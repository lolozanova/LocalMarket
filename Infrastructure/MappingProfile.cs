using AutoMapper;
using LocalMarket.Models.Home;
using LocalMarket.Models.Producer;
using LocalMarket.Services.Statistics;
using LocalMarket.Data.Models;
using LocalMarket.Models.Product.AddProduct;
using LocalMarket.Services.Products;
using LocalMarket.Models.Product.AllProducts;

namespace LocalMarket.Infrastructure
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<StatisticsServiceModel, IndexViewModel>().ReverseMap();
            CreateMap<CreateProducerFormModel, Producer>().ReverseMap();
            CreateMap<Town, TownViewModel>().ReverseMap();
            CreateMap<ProductFormModel, Product>().ReverseMap();
            CreateMap<ProductServiceModel, Product>().ReverseMap();
            CreateMap<ProductServiceModel, ProductFormModel>().ReverseMap();
            CreateMap<ProductServiceModel, ProductViewModel>().ReverseMap();

        }
    }
}
