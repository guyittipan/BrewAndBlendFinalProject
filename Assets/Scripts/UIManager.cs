
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour {
    [Header("Gameplay UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text comboText;



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
