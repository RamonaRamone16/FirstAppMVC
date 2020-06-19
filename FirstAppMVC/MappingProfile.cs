using AutoMapper;
using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;

namespace FirstAppMVC
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            ProductToProductModelMap();
            ProductToBasketItemMap();
            ProductCreateModelToProductMap();
            CategoryToCategoryModelMap();
            CategoryCreateModelToCategoryMap();
            BrandToBrandModelMap();
            BrandCreateModelToBrandMap();
            ProductEditModelMap<Product, ProductEditModel>();
            ProductToOrderCreateModelMap();
            OrderCreateModelToOrderMap();
            OrderToOrderModelMap();
        }
        public void ProductToProductModelMap()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(to => to.Brand, from => from.MapFrom
                (p => p.BrandId == null ? ProductModel.NoBrand : p.Brand.Name))
                .ForMember(to => to.Category, from => from.MapFrom(p => p.Category.Name))
                .ForMember(to => to.OrdersCount, from => from.MapFrom(p => p.Orders.Count));
        }

        public void ProductToBasketItemMap()
        {
            CreateMap<Product, BasketItem>()
                .ForMember(to => to.ProductId, from => from.MapFrom(p => p.Id))
                .ForMember(to => to.ProductName, from => from.MapFrom(p => p.Name))
                .ForMember(to => to.Brand, from => from.MapFrom
                (p => p.BrandId == null ? BasketItem.NoBrand : p.Brand.Name));
        }

        public void ProductCreateModelToProductMap()
        {
            CreateMap<ProductCreateModel, Product>();
        }

        public void CategoryToCategoryModelMap()
        {
            CreateMap<Category, CategoryModel>();
        }

        public void CategoryCreateModelToCategoryMap()
        {
            CreateMap<CategoryCreateModel, Category>();
        }

        public void BrandToBrandModelMap()
        {
            CreateMap<Brand, BrandModel>();
        }
        public void BrandCreateModelToBrandMap()
        {
            CreateMap<BrandCreateModel, Brand>();
        }

        public void ProductEditModelMap<From, To>()
        {
            CreateMap<From, To>();
            CreateMap<To, From>();
        }

        public void ProductToOrderCreateModelMap()
        {
            CreateMap<Product, OrderCreateModel>()
                .ForMember(to => to.ProductId, from => from.MapFrom(p => p.Id))
                .ForMember(to => to.ProductName, from => from.MapFrom(p => p.Name))
                .ForMember(to => to.Brand, from => from.MapFrom(p => p.Brand.Name))
                .ForMember(to => to.ProductPrice, from => from.MapFrom(p => p.Price));
        }

        public void OrderCreateModelToOrderMap()
        {
            CreateMap<OrderCreateModel, Order>();
        }

        public void OrderToOrderModelMap()
        {
            CreateMap<Order, OrderModel>()
                .ForMember(to => to.ProductName, from => from.MapFrom(p => p.Product.Name))
                .ForMember(to => to.Brand, from => from.MapFrom(p => p.Product.Brand.Name))
                .ForMember(to => to.ProductPrice, from => from.MapFrom(p => p.Product.Price));
        }

    }
}
