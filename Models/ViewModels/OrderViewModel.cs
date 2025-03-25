namespace CookiesKitchen.Models.ViewModels
{
    public class OrderViewModel
    {
        public Customer Customer { get; set; }
        public int SelectedMealId { get; set; }
        public List<Meal>? Meals { get; set; }
    }
}
