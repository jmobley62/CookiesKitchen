namespace CookiesKitchen.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Meal Meal { get; set; }

        public Order()
        {

        }
    }
}
