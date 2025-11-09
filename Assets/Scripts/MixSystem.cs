
using UnityEngine;

public class MixSystem : MonoBehaviour {
    [SerializeField] private RecipeDatabase recipeDB;
    [SerializeField] private Board board;
    private Cell selectedA;
    private Cell selectedB;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private ComboSystem comboSystem;

    public void SelectCell(Cell cell) {
        if (selectedA == null) {
            selectedA = cell;
            return;
        }
        if (selectedB == null && cell != selectedA) {
            selectedB = cell;
            TryMix();
            return;
        }
        // reset selections: choose new pair (last two)
        selectedA = selectedB;
        selectedB = cell;
        TryMix();
    }

    private void TryMix() {
        if (selectedA == null || selectedB == null) return;
        var ia = selectedA.CurrentIngredient?.Type ?? IngredientType.None;
        var ib = selectedB.CurrentIngredient?.Type ?? IngredientType.None;

        var recipe = recipeDB.GetRecipe(ia, ib);
        if (recipe != null) {
            int comboBonus = comboSystem != null ? comboSystem.UpdateCombo(recipe.recipeName) : 0;
            scoreSystem?.AddScore(recipe.baseScore, comboBonus);
            GameObject.Destroy(selectedA.CurrentIngredient?.gameObject);
            GameObject.Destroy(selectedB.CurrentIngredient?.gameObject);
            selectedA.Clear(); selectedB.Clear();
            board.RefillEmpty();
        } else {
            comboSystem?.ResetCombo();
        }

        selectedA = null;
        selectedB = null;
    }
}
