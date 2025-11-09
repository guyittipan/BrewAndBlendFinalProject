
using System.Collections.Generic;
using UnityEngine;

public class RecipeDatabase : MonoBehaviour {
    [SerializeField] private List<Recipe> recipes = new List<Recipe>();

    public List<Recipe> Recipes => recipes;

    public Recipe GetRecipe(IngredientType a, IngredientType b) {
        if (a == IngredientType.None || b == IngredientType.None) return null;
        foreach (var r in recipes) {
            if (r != null && r.Matches(a, b)) return r;
        }
        return null;
    }
}
