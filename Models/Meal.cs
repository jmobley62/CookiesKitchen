using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookiesKitchen.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount needs to be higher than 0")]
        public double Price { get; set; }
        public List<Order>? Orders { get; set; }

        public Meal()
        {

        }
    }
}
