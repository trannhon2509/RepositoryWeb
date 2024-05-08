namespace RevibeCO.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductImageUrl { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CouponProduct> CouponProducts { get; set; }
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; }
    }
}
