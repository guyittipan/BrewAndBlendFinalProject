using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController I { get; private set; }

    [SerializeField] private TimerSystem timer;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private RankSystem rankSystem;

    // ให้ ScoreScene เอาไปใช้แสดง
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

    // ---------- ฟัง sceneLoaded เพื่อรีเซ็ตตอนเข้า Gameplay ใหม่ ----------
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ทุกครั้งที่เข้า GameplayScene ให้ผูก reference ใหม่ + รีเซ็ตเกม
        if (scene.name == "GameplayScene")
        {
            // หา component ชุดใหม่ใน Scene นี้
            timer       = FindObjectOfType<TimerSystem>();
            scoreSystem = FindObjectOfType<ScoreSystem>();
            rankSystem  = FindObjectOfType<RankSystem>();

            var combo   = FindObjectOfType<ComboSystem>();
            var board   = FindObjectOfType<Board>();

            // รีเซ็ตค่าต่าง ๆ
            if (scoreSystem != null) scoreSystem.ResetScore();
            if (combo != null)       combo.ResetCombo();
            if (timer != null)       timer.ResetTimer();   // ตัว TimerSystem.Start จะ StartTimer ให้เองอยู่แล้ว

            // เติมกระดานใหม่ให้ชัวร์
            if (board != null)       board.RefillAll();
        }
    }
    // ----------------------------------------------------------------------


    // ฟังก์ชันโหลดซีนต่าง ๆ
    public void LoadEntrance() => SceneManager.LoadScene("Entrance");
    public void LoadTutorial() => SceneManager.LoadScene("TutorialScene");
    public void LoadGameplay() => SceneManager.LoadScene("GameplayScene");
    public void LoadScore()    => SceneManager.LoadScene("ScoreScene");
    public void LoadThank()    => SceneManager.LoadScene("ThankScene");

    public void StartGameplay()
    {
        LoadGameplay();
    }

    public void EndGame()
    {
        // ดึงคะแนนจากระบบตอนจบเกม
        int score = scoreSystem != null ? scoreSystem.TotalScore : 0;
        string rank = rankSystem != null ? rankSystem.GetRank(score) : "Unranked";

        // เก็บไว้ให้ซีนถัดไปใช้
        LastScore = score;
        LastRank  = rank;

        Debug.Log($"Game End. Score {score}, Rank {rank}");

        // ไปหน้าโชว์คะแนน
        LoadScore();
    }
}
