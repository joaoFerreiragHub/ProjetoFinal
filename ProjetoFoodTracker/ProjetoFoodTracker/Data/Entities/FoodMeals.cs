﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFoodTracker.Data.Entities
{
    public class FoodMeals
    {
        public int FoodMealsId { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}