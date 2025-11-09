
using UnityEngine;
using UnityEngine.UI;

public class ThankSceneController : MonoBehaviour {
    [SerializeField] private Text scoreText;
    [SerializeField] private Text rankText;
    [SerializeField] private Button backButton;
    [SerializeField] private GameController gameController;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private RankSystem rankSystem;

    private void Start() {
        if (backButton != null) backButton.onClick.AddListener(OnBack);
        Refresh();
    }

    private void Refresh() {
        int score = scoreSystem != null ? scoreSystem.TotalScore : 0;
        string rank = rankSystem != null ? rankSystem.GetRank(score) : "Unranked";
        if (scoreText != null) scoreText.text = score.ToString();
        if (rankText != null) rankText.text = rank;
    }

    private void OnBack() {
        gameController.LoadEntrance();
    }
}
