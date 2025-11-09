
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController I { get; private set; }

    [SerializeField] private TimerSystem timer;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private RankSystem rankSystem;

    private void Awake() {
        if (I == null) { I = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    public void LoadEntrance() => SceneManager.LoadScene("EntranceScene");
    public void LoadTutorial() => SceneManager.LoadScene("TutorialScene");
    public void LoadGameplay() => SceneManager.LoadScene("GameplayScene");
    public void LoadThank() => SceneManager.LoadScene("ThankScene");

    public void StartGameplay() {
        LoadGameplay();
    }

    public void EndGame() {
        int score = scoreSystem != null ? scoreSystem.TotalScore : 0;
        string rank = rankSystem != null ? rankSystem.GetRank(score) : "Unranked";
        Debug.Log($"Game End. Score {score}, Rank {rank}");
        LoadThank();
    }
}
