namespace RevibeCO.Models
{
    public class FavoriteProduct
    {
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime DateAdded { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
