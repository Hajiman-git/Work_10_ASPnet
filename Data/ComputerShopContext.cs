using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using work_10.Models;

namespace work_10.Data;

public partial class ComputerShopContext : DbContext
{
    public ComputerShopContext()
    {
    }

    public ComputerShopContext(DbContextOptions<ComputerShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StockReservation> StockReservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MEGABOOK_K16\\SQLEXPRESS;Initial Catalog=computer_shop;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__carts__701794903EE62AF8");

            entity.ToTable("carts");

            entity.HasIndex(e => e.CustomerId, "UQ__carts__CD64CFDCD9A335A8").IsUnique();

            entity.Property(e => e.IdCart).HasColumnName("ID_cart");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_ID");

            entity.HasOne(d => d.Customer).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__carts__customer___5535A963");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.IdCartItem).HasName("PK__cart_ite__2D2131966DCAE3AD");

            entity.ToTable("cart_items");

            entity.HasIndex(e => new { e.CartId, e.ProductId }, "UQ__cart_ite__EA8CD981375CD581").IsUnique();

            entity.Property(e => e.IdCartItem).HasColumnName("ID_cart_item");
            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("added_date");
            entity.Property(e => e.CartId).HasColumnName("cart_ID");
            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cart_item__cart___5AEE82B9");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cart_item__produ__5BE2A6F2");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__categori__AC5CA40E867AEF24");

            entity.ToTable("categories");

            entity.HasIndex(e => e.CategoryName, "UQ__categori__5189E255F7A33DB7").IsUnique();

            entity.Property(e => e.IdCategory).HasColumnName("ID_category");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("category_name");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__customer__3291C803C2118303");

            entity.ToTable("customers");

            entity.HasIndex(e => e.PhoneNumber, "UQ__customer__A1936A6BC47D0AFB").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__customer__AB6E61642C9D5718").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__customer__F3DBC572E873D63F").IsUnique();

            entity.Property(e => e.IdCustomer).HasColumnName("ID_customer");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("registration_date");
            entity.Property(e => e.RoleId)
                .HasDefaultValue(1)
                .HasColumnName("role_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Customers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__customers__role___44FF419A");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__orders__89BFCD59FBD3BEBE");

            entity.ToTable("orders");

            entity.Property(e => e.IdOrder).HasColumnName("ID_order");
            entity.Property(e => e.Comment)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.CustomerId).HasColumnName("customer_ID");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("delivery_address");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.StatusId)
                .HasDefaultValue(1)
                .HasColumnName("status_ID");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__customer__656C112C");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__status_I__66603565");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.IdOrderItem).HasName("PK__order_it__5B1127FF2AD11209");

            entity.ToTable("order_items");

            entity.Property(e => e.IdOrderItem).HasColumnName("ID_order_item");
            entity.Property(e => e.OrderId).HasColumnName("order_ID");
            entity.Property(e => e.PriceAtTime)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price_at_time");
            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_ite__order__6B24EA82");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_ite__produ__6C190EBB");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PK__order_st__449BD7E61BC73439");

            entity.ToTable("order_statuses");

            entity.HasIndex(e => e.StatusName, "UQ__order_st__501B37539A5B8A39").IsUnique();

            entity.Property(e => e.IdStatus).HasColumnName("ID_status");
            entity.Property(e => e.StatusName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__products__FD7FEC23B54ACA05");

            entity.ToTable("products");

            entity.Property(e => e.IdProduct).HasColumnName("ID_product");
            entity.Property(e => e.CategoryId).HasColumnName("category_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.IsService).HasColumnName("is_service");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__products__catego__5070F446");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__roles__45DFFBDB9D03AF3A");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "UQ__roles__783254B1C508DE83").IsUnique();

            entity.Property(e => e.IdRole).HasColumnName("ID_role");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<StockReservation>(entity =>
        {
            entity.HasKey(e => e.IdReservation).HasName("PK__stock_re__AAC2FDAF70C2F5BF");

            entity.ToTable("stock_reservations");

            entity.Property(e => e.IdReservation).HasColumnName("ID_reservation");
            entity.Property(e => e.CustomerId).HasColumnName("customer_ID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReleasedAt)
                .HasColumnType("datetime")
                .HasColumnName("released_at");
            entity.Property(e => e.ReservedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("reserved_at");

            entity.HasOne(d => d.Customer).WithMany(p => p.StockReservations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__stock_res__custo__73BA3083");

            entity.HasOne(d => d.Product).WithMany(p => p.StockReservations)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__stock_res__produ__72C60C4A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
