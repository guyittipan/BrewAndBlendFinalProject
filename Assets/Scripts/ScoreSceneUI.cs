using UnityEngine;
using TMPro;

public class ScoreSceneUI : MonoBehaviour
{
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        if (GameController.I != null)
        {
            // ดึงค่าจาก GameController
            if (rankText != null)
                rankText.text = $"Rank: {GameController.I.LastRank}";

            if (scoreText != null)
                scoreText.text = $"Score: {GameController.I.LastScore}";
        }
        else
        {
            // เผื่อเปิดซีนนี้ตรง ๆ ใน Editor
            if (rankText != null)
                rankText.text = "Rank: N/A";

            if (scoreText != null)
                scoreText.text = "Score: 0";
        }
    }
}
