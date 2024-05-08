namespace RevibeCO.Data
{
    using Microsoft.EntityFrameworkCore;
    using RevibeCO.Models;

    public class RevibeCoDbContext : DbContext
    {
        public RevibeCoDbContext(DbContextOptions<RevibeCoDbContext> options) : base(options)
        {
        }

        // DbSet cho mỗi bảng trong cơ sở dữ liệu

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Coupon> Coupons { get; set; }

        public DbSet<CouponProduct> CouponProducts { get; set; }

        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteProduct>()
            .HasKey(fp => fp.FavoriteId); // Cài đặt khóa chính cho bảng FavoriteProduct

            modelBuilder.Entity<Coupon>()
                .Property(c => c.DiscountValue)
                .HasColumnType("decimal(18,2)"); // Độ chính xác 18 chữ số và 2 chữ số thập phân

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)"); // Độ chính xác 18 chữ số và 2 chữ số thập phân

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasColumnType("decimal(18,2)"); // Độ chính xác 18 chữ số và 2 chữ số thập phân

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductPrice)
                .HasColumnType("decimal(18,2)"); // Độ chính xác 18 chữ số và 2 chữ số thập phân

/*-------------------------------------------------** Cài đặt các mối quan hệ **-------------------------------------------------*/

            // 1. Mối quan hệ giữa Product và Category:
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)                     // Mỗi sản phẩm thuộc về một danh mục
                .WithMany(c => c.Products)                    // Một danh mục có thể chứa nhiều sản phẩm
                .HasForeignKey(p => p.CategoryId);           // Khóa ngoại CategoryId trong Product

            // 2. Mối quan hệ giữa OrderDetail và Order, Product:
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });   // Khóa chính của OrderDetail bao gồm OrderId và ProductId

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)                            // Mỗi chi tiết đơn hàng thuộc về một đơn hàng
                .WithMany(o => o.OrderDetails)                     // Một đơn hàng có thể có nhiều chi tiết đơn hàng
                .HasForeignKey(od => od.OrderId);                 // Khóa ngoại OrderId trong OrderDetail

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)                          // Mỗi chi tiết đơn hàng liên quan đến một sản phẩm
                .WithMany(p => p.OrderDetails)                     // Một sản phẩm có thể xuất hiện trong nhiều chi tiết đơn hàng
                .HasForeignKey(od => od.ProductId);               // Khóa ngoại ProductId trong OrderDetail

            // 3. Mối quan hệ giữa User và Address:
            modelBuilder.Entity<User>()
                .HasOne(u => u.DefaultAddress)              // Mỗi người dùng chỉ có một địa chỉ mặc định
                .WithMany(a => a.Users)                      // Một địa chỉ có thể thuộc về nhiều người dùng
                .HasForeignKey(u => u.DefaultAddressId);    // Khóa ngoại DefaultAddressId trong User

            // 4. Mối quan hệ giữa Rating và Product, User:
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Product)                      // Mỗi đánh giá liên quan đến một sản phẩm
                .WithMany(p => p.Ratings)                     // Một sản phẩm có thể có nhiều đánh giá
                .HasForeignKey(r => r.ProductId);           // Khóa ngoại ProductId trong Rating

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)                         // Mỗi đánh giá thuộc về một người dùng
                .WithMany(u => u.Ratings)                     // Một người dùng có thể có nhiều đánh giá
                .HasForeignKey(r => r.UserId);              // Khóa ngoại UserId trong Rating

            // 5. Mối quan hệ giữa Comment và Product, User:
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Product)                      // Mỗi bình luận thuộc về một sản phẩm
                .WithMany(p => p.Comments)                    // Một sản phẩm có thể có nhiều bình luận
                .HasForeignKey(c => c.ProductId);           // Khóa ngoại ProductId trong Comment

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)                         // Mỗi bình luận liên quan đến một người dùng
                .WithMany(u => u.Comments)                    // Một người dùng có thể có nhiều bình luận
                .HasForeignKey(c => c.UserId);              // Khóa ngoại UserId trong Comment

            // 6. Mối quan hệ giữa Coupon và CouponProduct, Product:
            modelBuilder.Entity<CouponProduct>()
                .HasKey(cp => new { cp.CouponId, cp.ProductId });   // Khóa chính của CouponProduct bao gồm CouponId và ProductId

            modelBuilder.Entity<CouponProduct>()
                .HasOne(cp => cp.Coupon)                            // Mỗi liên kết giữa mã giảm giá và sản phẩm thuộc về một mã giảm giá
                .WithMany(c => c.CouponProducts)                    // Một mã giảm giá có thể áp dụng cho nhiều sản phẩm
                .HasForeignKey(cp => cp.CouponId);                 // Khóa ngoại CouponId trong CouponProduct

            modelBuilder.Entity<CouponProduct>()
                .HasOne(cp => cp.Product)                           // Mỗi liên kết giữa mã giảm giá và sản phẩm thuộc về một sản phẩm
                .WithMany(p => p.CouponProducts)                    // Một sản phẩm có thể được áp dụng nhiều mã giảm giá
                .HasForeignKey(cp => cp.ProductId);                // Khóa ngoại ProductId trong CouponProduct

            // 7. Mối quan hệ giữa FavoriteProduct và User, Product:
            modelBuilder.Entity<FavoriteProduct>()
                .HasOne(fp => fp.User)                              // Mỗi sản phẩm yêu thích thuộc về một người dùng
                .WithMany(u => u.FavoriteProducts)                  // Một người dùng có thể có nhiều sản phẩm yêu thích
                .HasForeignKey(fp => fp.UserId);                   // Khóa ngoại UserId trong FavoriteProduct

            modelBuilder.Entity<FavoriteProduct>()
                .HasOne(fp => fp.Product)                           // Mỗi sản phẩm yêu thích thuộc về một sản phẩm
                .WithMany(p => p.FavoriteProducts)                  // Một sản phẩm có thể được yêu thích bởi nhiều người dùng
                .HasForeignKey(fp => fp.ProductId);                // Khóa ngoại ProductId trong FavoriteProduct

            // 8. Mối quan hệ giữa Role và User:
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)                              // Mỗi vai trò có thể được gán cho nhiều người dùng
                .WithOne(u => u.Role)                                // Mỗi người dùng chỉ có một vai trò
                .HasForeignKey(u => u.RoleId);                      // Khóa ngoại RoleId trong User

            // 9. Mối quan hệ giữa Order và User:
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)                         // Mỗi đơn hàng thuộc về một người dùng
                .WithMany(u => u.Orders)                     // Một người dùng có thể có nhiều đơn hàng
                .HasForeignKey(o => o.UserId);              // Khóa ngoại UserId trong Order

            // 10. Mối quan hệ giữa Coupon và CouponProduct:
            modelBuilder.Entity<CouponProduct>()
                .HasOne(cp => cp.Coupon)                           // Mỗi liên kết giữa mã giảm giá và sản phẩm thuộc về một mã giảm giá
                .WithMany(c => c.CouponProducts)                   // Một mã giảm giá có thể áp dụng cho nhiều sản phẩm
                .HasForeignKey(cp => cp.CouponId);                // Khóa ngoại CouponId trong CouponProduct

            // 11. Mối quan hệ giữa Comment và Product, User:
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)                               // Mỗi bình luận thuộc về một người dùng
                .WithMany(u => u.Comments)                          // Một người dùng có thể có nhiều bình luận
                .HasForeignKey(c => c.UserId);                    // Khóa ngoại UserId trong Comment

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Product)                             // Mỗi bình luận thuộc về một sản phẩm
                .WithMany(p => p.Comments)                          // Một sản phẩm có thể có nhiều bình luận
                .HasForeignKey(c => c.ProductId);                  // Khóa ngoại ProductId trong Comment
        }
    }
}
