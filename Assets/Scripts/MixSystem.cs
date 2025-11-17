using UnityEngine;

public class MixSystem : MonoBehaviour
{
    [SerializeField] private RecipeDatabase recipeDB;
    [SerializeField] private Board board;
    private Cell selectedA;
    private Cell selectedB;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private ComboSystem comboSystem;
    [SerializeField] private TimerSystem timerSystem;   


    [Header("UI")]
    [SerializeField] private MixResultUI mixResultUI;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip burntCoffeeSound;   // 👈 เพิ่มอันนี้



    public void SelectCell(Cell cell)
{
    if (cell == null || cell.CurrentIngredient == null)
        return;

    var type = cell.CurrentIngredient.Type;

    // 1) Burnt Coffee (debuff)
    if (type == IngredientType.BurntCoffee)
    {
        OnSelectBurntCoffee(cell);
        return;
    }

    // 2) Golden Milk (เพิ่มเวลา)
    if (type == IngredientType.GoldenMilk)
    {
        OnSelectGoldenMilk(cell);
        return;
    }

    // 3) Reroll Chocolate (สุ่มทั้งกระดานใหม่)
    if (type == IngredientType.RerollChocolate)
    {
        OnSelectRerollChocolate(cell);
        return;
    }

    // ------------------ เคสปกติ (ส่วนผสมจริง) ------------------
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
  private void OnSelectBurntCoffee(Cell cell)
{
    Debug.Log("Burnt Coffee clicked! Time debuff!");

    // 1) ลดเวลา 5 วินาที
    if (timerSystem != null)
    {
        timerSystem.ReduceTime(5f);
    }

    // 2) รีเซ็ตคอมโบ
    if (comboSystem != null)
    {
        comboSystem.ResetCombo();
    }

    // 3) เขย่ากล้อง
    var shaker = FindObjectOfType<CameraShaker>();
    if (shaker != null)
    {
        shaker.Shake();
    }

    // 4) ลบ ingredient ออกจาก cell
    if (cell.CurrentIngredient != null)
    {
        Destroy(cell.CurrentIngredient.gameObject);
    }
    cell.Clear();

    // 5) เติมช่องว่าง
    if (board != null)
    {
        board.RefillEmpty();
    }

    // 6) รีเซ็ตตัวเลือก
    selectedA = null;
    selectedB = null;

    if (audioSource != null && burntCoffeeSound != null) {
    audioSource.PlayOneShot(burntCoffeeSound);
}

}
private void OnSelectGoldenMilk(Cell cell)
{
    Debug.Log("Golden Milk clicked! Time +5s");

    // 1) เพิ่มเวลา
    if (timerSystem != null)
    {
        timerSystem.AddTime(5f);   // ปรับจำนวนวินาทีได้ตามใจ
    }

    // 2) อาจจะให้โบนัสคอมโบ/คะแนนเล็กน้อยก็ได้ แต่ตอนนี้เอาแค่เพิ่มเวลา

    // 3) ลบ ingredient ออกจาก cell
    if (cell.CurrentIngredient != null)
    {
        Destroy(cell.CurrentIngredient.gameObject);
    }
    cell.Clear();

    // 4) เติมของใหม่ในช่องนี้
    if (board != null)
    {
        board.RefillEmpty();
    }

    // ไม่ต้องยุ่งกับ selectedA/B เพราะมันไม่ใช่ส่วนผสมใช้ผสม
    selectedA = null;
    selectedB = null;
}
private void OnSelectRerollChocolate(Cell cell)
{
    Debug.Log("Reroll Chocolate clicked! Reroll all board!");

    // 1) ลบตัว reroll เองจาก cell (เผื่อ Board.RerollAll ไม่จัดการ)
    if (cell.CurrentIngredient != null)
    {
        Destroy(cell.CurrentIngredient.gameObject);
    }
    cell.Clear();

    // 2) เรียกให้ Board สุ่มใหม่ทั้งกระดาน
    if (board != null)
    {
        board.RerollAll();
    }

    // 3) รีเซ็ตตัวเลือก A/B
    selectedA = null;
    selectedB = null;

    // ถ้าอยากมี SFX หรือกล้องเขย่าก็เพิ่มตรงนี้ได้
    // เช่น shaker.Shake(0.3f, 0.2f);
}



}
