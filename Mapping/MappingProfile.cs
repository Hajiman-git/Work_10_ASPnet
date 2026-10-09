using AutoMapper;
using work_10.Dtos.Cart;
using work_10.Dtos.CartItem;
using work_10.Dtos.Category;
using work_10.Dtos.Customer;
using work_10.Dtos.Order;
using work_10.Dtos.OrderItem;
using work_10.Dtos.OrderStatus;
using work_10.Dtos.Product;
using work_10.Dtos.Role;
using work_10.Dtos.StockReservation;
using work_10.Models;

namespace work_10.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();


            CreateMap<OrderStatus, OrderStatusDto>();
            CreateMap<CreateOrderStatusDto, OrderStatus>();
            CreateMap<UpdateOrderStatusDto, OrderStatus>();


            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();


            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<CreateCustomerDto, Customer>()
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Cart, opt => opt.Ignore())
                .ForMember(dest => dest.Orders, opt => opt.Ignore());

            CreateMap<UpdateCustomerDto, Customer>()
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Cart, opt => opt.Ignore())
                .ForMember(dest => dest.Orders, opt => opt.Ignore());


            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.CartItems, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
                .ForMember(dest => dest.StockReservations, opt => opt.Ignore());

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.CartItems, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
                .ForMember(dest => dest.StockReservations, opt => opt.Ignore());


            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));

            CreateMap<CreateCartDto, Cart>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.CartItems, opt => opt.Ignore());

            CreateMap<UpdateCartDto, Cart>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.CartItems, opt => opt.Ignore());

            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<CreateCartItemDto, CartItem>()
                .ForMember(dest => dest.AddedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Cart, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore());

            CreateMap<UpdateCartItemDto, CartItem>()
                .ForMember(dest => dest.AddedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Cart, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore());


            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());

            CreateMap<UpdateOrderDto, Order>()
                .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<CreateOrderItemDto, OrderItem>()
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore());

            CreateMap<UpdateOrderItemDto, OrderItem>()
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore());

            CreateMap<StockReservation, StockReservationDto>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));


            CreateMap<ReserveStockDto, StockReservation>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.ReservedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ReleasedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore());
        }
    }
}