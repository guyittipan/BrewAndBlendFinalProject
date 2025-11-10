using UnityEngine;
using System;
using UnityEngine.SceneManagement;   // สำหรับโหลด Scene

public class TimerSystem : MonoBehaviour {
    [SerializeField] private float timeLimit = 120f;
    [SerializeField] private float delayBeforeEnd = 1.5f;   // หน่วงเวลาก่อนเปลี่ยน Scene

    private float remaining;
    private bool running = false;
    private UIManager uiManager;

    public event Action OnTimeUp;
    public event Action<float> OnTick;

    public float Remaining => remaining;

    private void Start() {
        uiManager = FindObjectOfType<UIManager>();

        // เซ็ตเวลาเริ่มต้นและเริ่มนับทันทีเมื่อเข้า Scene
        ResetTimer();
        StartTimer();
    }

    public void StartTimer() {
        remaining = timeLimit;
        running = true;
        uiManager?.UpdateTimer(remaining);
    }

    public void ResetTimer() {
        remaining = timeLimit;
        running = false;
        uiManager?.UpdateTimer(remaining);
    }

    private void Update() {
        if (!running) return;

        remaining -= Time.deltaTime;
        if (remaining < 0) remaining = 0;

        // อัปเดตเวลาใน UI ทุกเฟรม
        uiManager?.UpdateTimer(remaining);
        OnTick?.Invoke(remaining);

        if (remaining <= 0f) {
            running = false;
            OnTimeUp?.Invoke();
            StartCoroutine(GoToScoreAfterDelay());
        }
    }

    private System.Collections.IEnumerator GoToScoreAfterDelay() {
        yield return new WaitForSeconds(delayBeforeEnd);

        // ถ้ามี GameController ให้ใช้ของมันก่อน
        var gc = FindObjectOfType<GameController>();
        if (gc != null) {
            gc.EndGame();
        } 
    }
}
