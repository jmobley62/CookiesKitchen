﻿namespace CookiesKitchen.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public int Price { get; set; }

        public Meal()
        {

        }
    }
}
