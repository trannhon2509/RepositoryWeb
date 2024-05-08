namespace RevibeCO.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool IsDefaultAddress { get; set; }

        public ICollection<User> Users { get; set; } // Thêm thuộc tính Users ở đây
    }
}
