namespace CookiesKitchen.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? MealId { get; set; }
        public Meal Meal { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public Order()
        {

        }
    }
}
