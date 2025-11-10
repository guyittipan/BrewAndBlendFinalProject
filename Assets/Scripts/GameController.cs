using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController I { get; private set; }

    [SerializeField] private TimerSystem timer;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private RankSystem rankSystem;

    // 👇 ตัวที่จะให้ ScoreScene เอาไปแสดง
    public int LastScore { get; private set; }
    public string LastRank { get; private set; }

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadEntrance() => SceneManager.LoadScene("EntranceScene");
    public void LoadTutorial() => SceneManager.LoadScene("TutorialScene");
    public void LoadGameplay() => SceneManager.LoadScene("GameplayScene");
    public void LoadScore() => SceneManager.LoadScene("ScoreScene");
    public void LoadThank() => SceneManager.LoadScene("ThankScene");

    public void StartGameplay()
    {
        LoadGameplay();
    }

    public void EndGame()
    {
        // ดึงคะแนนจากระบบตอนจบเกม
        int score = scoreSystem != null ? scoreSystem.TotalScore : 0;
        string rank = rankSystem != null ? rankSystem.GetRank(score) : "Unranked";

        // ✅ เก็บไว้ให้ซีนถัดไปใช้
        LastScore = score;
        LastRank = rank;

        Debug.Log($"Game End. Score {score}, Rank {rank}");

        // ไปหน้าโชว์คะแนนก่อน
        LoadScore();
    }
}
