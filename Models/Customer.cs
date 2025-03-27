namespace CookiesKitchen.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { set; get; } = "North Carolina";
        public string Email { set; get; }
        public string PhoneNumber { get; set; }
        public List<Order> Orders { get; set; } = [];

        public Customer()
        {

        }

    }
}
