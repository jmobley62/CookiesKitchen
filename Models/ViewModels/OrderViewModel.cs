namespace CookiesKitchen.Models.ViewModels
{
    public class OrderViewModel
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Meal> Meals { get; set; } = new List<Meal>();
        public Order Order { get; set; } = new Order();
    }
}
