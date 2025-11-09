
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Game/Recipe")]
public class Recipe : ScriptableObject {
    public string recipeName;
    public int baseScore;
    public IngredientType ingredientA;
    public IngredientType ingredientB;

    public bool Matches(IngredientType a, IngredientType b) {
        return (ingredientA == a && ingredientB == b) || (ingredientA == b && ingredientB == a);
    }
}
