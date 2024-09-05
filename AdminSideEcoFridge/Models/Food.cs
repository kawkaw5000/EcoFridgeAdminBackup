using System;
using System.Collections.Generic;

namespace AdminSideEcoFridge.Models;

public partial class Food
{
    public int FoodId { get; set; }

    public int? FoodCategoryId { get; set; }

    public string? FoodName { get; set; }

    public int? Quantity { get; set; }

    public int? Servings { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string? FoodPicturePath { get; set; }

    public virtual ICollection<FoodIngredient> FoodIngredients { get; set; } = new List<FoodIngredient>();

    public virtual ICollection<UserFood> UserFoods { get; set; } = new List<UserFood>();
}
