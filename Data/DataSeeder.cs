using RevibeCO.Models;

namespace RevibeCO.Data
{
    public class DataSeeder
    {
        private readonly RevibeCoDbContext _context;
        private readonly Random _random;

        public DataSeeder(RevibeCoDbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public void SeedData()
        {
            SeedRoles();
            _context.SaveChanges();
            SeedUsers(100);
            _context.SaveChanges();
        }

        private void SeedRoles()
        {
            if (!_context.Roles.Any())
            {
                _context.Roles.AddRange(
                    new Role { RoleName = "User" },
                    new Role { RoleName = "Blogger" },
                    new Role { RoleName = "Admin" }
                );

            }
        }

        private void SeedUsers(int count)
        {
            if (!_context.Users.Any())
            {
                for (int i = 0; i < count; i++)
                {
                    var user = new User
                    {
                        RoleId = _random.Next(1, (_context.Roles.Count() + 1)), // Chọn ngẫu nhiên một trong ba vai trò
                        Username = GenerateRandomString(8),
                        Email = GenerateRandomEmail(),
                        Password = GenerateRandomString(10),
                        DefaultAddress = GenerateRandomAddress(),
                        PhoneNumber = GenerateRandomPhoneNumber() // Cung cấp số điện thoại ngẫu nhiên
                    };
                    _context.Users.Add(user);
                }

            }
        }

        private Address GenerateRandomAddress()
        {
            return new Address
            {
                AddressLine1 = GenerateRandomString(12),
                AddressLine2 = "N/A", // Giá trị mặc định hoặc tùy chọn khác
                City = GenerateRandomString(6),
                State = GenerateRandomString(6),
                Country = "Vietnam"
            };
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string GenerateRandomEmail()
        {
            // Danh sách các tên miền email phổ biến
            string[] emailDomains = { "example.com", "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "aol.com" };

            // Chọn ngẫu nhiên một tên miền từ danh sách
            string randomDomain = emailDomains[_random.Next(emailDomains.Length)];

            // Tạo email ngẫu nhiên với tên người dùng và tên miền được chọn
            return $"{GenerateRandomString(8)}@{randomDomain}";
        }
        private string GenerateRandomPhoneNumber()
        {
            Random random = new Random();
            return "0" + random.Next(100000000, 999999999).ToString(); // Tạo số điện thoại ngẫu nhiên trong khoảng từ 100,000,000 đến 999,999,999
        }
    }
}
