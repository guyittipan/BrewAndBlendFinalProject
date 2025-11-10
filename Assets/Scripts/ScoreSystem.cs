using UnityEngine;

public class ScoreSystem : MonoBehaviour {
    private int totalScore = 0;
    private UIManager uiManager;

    public int TotalScore => totalScore;

    private void Start() {
        // หาตัว UIManager ในฉาก
        uiManager = FindObjectOfType<UIManager>();

        // อัปเดตค่าเริ่มต้นให้โชว์ 0 ก่อน
        if (uiManager != null) {
            uiManager.UpdateScore(totalScore);
        } else {
            Debug.LogWarning("ScoreSystem: UIManager not found in scene.");
        }
    }

    public void AddScore(int baseScore, int comboBonus) {
        int add = baseScore + comboBonus;
        totalScore += add;
        Debug.Log($"Added score: {add} (base {baseScore} + combo {comboBonus}). Total: {totalScore}");

        // อัปเดตไปที่ UI ทันที
        if (uiManager != null) {
            uiManager.UpdateScore(totalScore);
        }
    }

    public void ResetScore() {
        totalScore = 0;
        if (uiManager != null) {
            uiManager.UpdateScore(totalScore);
        }
    }
}
