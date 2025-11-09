
using UnityEngine;

public class ComboSystem : MonoBehaviour {
    private string lastRecipe = string.Empty;
    private int comboCount = 0;
    [SerializeField] private int baseComboBonus = 10;

    public int UpdateCombo(string recipeName) {
        if (recipeName == lastRecipe) {
            comboCount++;
        } else {
            lastRecipe = recipeName;
            comboCount = 1;
        }
        int bonus = baseComboBonus * (comboCount - 1);
        Debug.Log($"Combo {comboCount} for {recipeName}. Bonus {bonus}");
        return bonus;
    }

    public void ResetCombo() {
        lastRecipe = string.Empty;
        comboCount = 0;
    }

    public int GetComboCount() => comboCount;
}
