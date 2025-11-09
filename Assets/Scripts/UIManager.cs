
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [Header("Gameplay UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text comboText;

    public void UpdateScore(int score) {
        if (scoreText != null) scoreText.text = score.ToString();
    }

    public void UpdateTimer(float remaining) {
        if (timerText != null) timerText.text = Mathf.CeilToInt(remaining).ToString();
    }

    public void UpdateCombo(int combo) {
        if (comboText != null) comboText.text = combo > 1 ? "Combo x" + combo : "";
    }
}
