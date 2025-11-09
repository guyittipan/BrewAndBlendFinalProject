
using UnityEngine;
using System;

public class TimerSystem : MonoBehaviour {
    [SerializeField] private float timeLimit = 120f;
    private float remaining;
    public event Action OnTimeUp;
    public event Action<float> OnTick;

    private bool running = false;

    public float Remaining => remaining;

    private void Start() {
        ResetTimer();
    }

    public void StartTimer() {
        remaining = timeLimit;
        running = true;
    }

    public void ResetTimer() {
        remaining = timeLimit;
        running = false;
    }

    private void Update() {
        if (!running) return;
        remaining -= Time.deltaTime;
        OnTick?.Invoke(remaining);
        if (remaining <= 0f) {
            running = false;
            remaining = 0f;
            OnTimeUp?.Invoke();
        }
    }
}
