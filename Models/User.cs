namespace RevibeCO.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int DefaultAddressId { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }
        public Address DefaultAddress { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; }
    }
}
