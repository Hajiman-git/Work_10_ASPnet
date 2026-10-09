using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using work_10.Data;
using work_10.Repositories;
using work_10.Services;
namespace work_10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ComputerShopContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IStockReservationRepository, StockReservationRepository>();

            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICartItemService, CartItemService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IStockReservationService, StockReservationService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}