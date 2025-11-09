
using UnityEngine;

public class Ingredient : MonoBehaviour {
    [SerializeField] private IngredientType type = IngredientType.None;
    [SerializeField] private string ingredientName;

    public IngredientType Type => type;
    public string IngredientName => string.IsNullOrEmpty(ingredientName) ? type.ToString() : ingredientName;
}
