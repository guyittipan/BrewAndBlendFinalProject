
using UnityEngine;

public class ScoreSystem : MonoBehaviour {
    private int totalScore = 0;

    public int TotalScore => totalScore;

    public void AddScore(int baseScore, int comboBonus) {
        int add = baseScore + comboBonus;
        totalScore += add;
        Debug.Log($"Added score: {add} (base {baseScore} + combo {comboBonus}). Total: {totalScore}");
    }

    public void ResetScore() {
        totalScore = 0;
    }
}
