using System.ComponentModel.DataAnnotations.Schema;

namespace CookiesKitchen.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public List<Order>? Orders { get; set; }

        public Meal()
        {

        }
    }
}
