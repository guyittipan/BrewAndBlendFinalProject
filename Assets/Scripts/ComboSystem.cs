using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    private string lastRecipe = string.Empty;
    private int comboCount = 0;
    [SerializeField] private float maxMultiplier = 5f; // จำกัดคอมโบสูงสุด เช่น คูณ5

    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

        if (uiManager != null)
            uiManager.UpdateCombo(0);
    }

    // เรียกตอนผสมสำเร็จ
    public float UpdateCombo(string recipeName)
    {
        if (recipeName == lastRecipe)
        {
            comboCount++;
        }
        else
        {
            lastRecipe = recipeName;
            comboCount = 1;
        }

        // คูณตามคอมโบ แต่ไม่เกิน maxMultiplier
        float multiplier = Mathf.Min(comboCount, maxMultiplier);

        if (uiManager != null)
            uiManager.UpdateCombo(comboCount);

        Debug.Log($"Combo {comboCount}x for {recipeName}. Multiplier = {multiplier}");
        return multiplier;
    }

    // เรียกตอนผสมไม่สำเร็จหรือเปลี่ยนสูตร
    public void ResetCombo()
    {
        lastRecipe = string.Empty;
        comboCount = 0;

        if (uiManager != null)
            uiManager.UpdateCombo(0);
    }

    public int GetComboCount() => comboCount;
}
