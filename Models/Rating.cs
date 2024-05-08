namespace RevibeCO.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int RatingValue { get; set; }
        public DateTime RatingDate { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
