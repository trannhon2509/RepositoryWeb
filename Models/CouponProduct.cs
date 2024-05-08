namespace RevibeCO.Models
{
    public class CouponProduct
    {
        public int CouponId { get; set; }
        public int ProductId { get; set; }

        public Coupon Coupon { get; set; }
        public Product Product { get; set; }
    }
}
