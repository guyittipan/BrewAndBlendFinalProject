
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour {
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TimerSystem timer;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private ComboSystem comboSystem;

    private void Start() {
        if (timer != null) timer.OnTick += OnTick;
        if (timer != null) timer.OnTimeUp += OnTimeUp;
        UpdateAll();
    }

    private void OnDestroy() {
        if (timer != null) timer.OnTick -= OnTick;
        if (timer != null) timer.OnTimeUp -= OnTimeUp;
    }

    private void OnTick(float remaining) {
        uiManager?.UpdateTimer(remaining);
    }

    private void OnTimeUp() {
        // let GameController handle end
    }

    public void UpdateAll() {
        uiManager?.UpdateScore(scoreSystem != null ? scoreSystem.TotalScore : 0);
        uiManager?.UpdateCombo(comboSystem != null ? comboSystem.GetComboCount() : 0);
        uiManager?.UpdateTimer(timer != null ? timer.Remaining : 0f);
    }
}
