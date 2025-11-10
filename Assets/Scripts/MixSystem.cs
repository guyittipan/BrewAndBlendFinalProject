using UnityEngine;

public class MixSystem : MonoBehaviour
{
    [SerializeField] private RecipeDatabase recipeDB;
    [SerializeField] private Board board;
    private Cell selectedA;
    private Cell selectedB;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private ComboSystem comboSystem;

    [Header("UI")]
    [SerializeField] private MixResultUI mixResultUI;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip successSound;

    public void SelectCell(Cell cell)
    {
        if (selectedA == null)
        {
            selectedA = cell;
            return;
        }

        if (selectedB == null && cell != selectedA)
        {
            selectedB = cell;
            TryMix();
            return;
        }

        // ถ้าคลิกอันที่ 3 → ใช้สองอันล่าสุด
        selectedA = selectedB;
        selectedB = cell;
        TryMix();
    }

    private void TryMix()
    {
        if (selectedA == null || selectedB == null) return;

        var ia = selectedA.CurrentIngredient?.Type ?? IngredientType.None;
        var ib = selectedB.CurrentIngredient?.Type ?? IngredientType.None;

        var recipe = recipeDB.GetRecipe(ia, ib);
        if (recipe != null)
        {
            // เอาคูณคอมโบจาก ComboSystem
            float comboMultiplier = comboSystem != null ? comboSystem.UpdateCombo(recipe.recipeName) : 1f;
            int finalScore = Mathf.RoundToInt(recipe.baseScore * comboMultiplier);

            // ✅ ScoreSystem ต้องการ 2 ตัว เลยส่ง 0 ไปตัวที่สอง
            scoreSystem?.AddScore(finalScore, 0);

            // ลบของเก่า + เติมใหม่
            Destroy(selectedA.CurrentIngredient?.gameObject);
            Destroy(selectedB.CurrentIngredient?.gameObject);
            selectedA.Clear();
            selectedB.Clear();
            board.RefillEmpty();

            // เสียง
            if (audioSource != null && successSound != null)
                audioSource.PlayOneShot(successSound);

            // รูปเมนู
            if (mixResultUI != null)
                mixResultUI.Show(recipe.recipeName);
        }
        else
        {
            comboSystem?.ResetCombo();
        }

        selectedA = null;
        selectedB = null;
    }
}
