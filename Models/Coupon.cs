namespace RevibeCO.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool ActiveStatus { get; set; }

        public ICollection<CouponProduct> CouponProducts { get; set; }
    }
}
