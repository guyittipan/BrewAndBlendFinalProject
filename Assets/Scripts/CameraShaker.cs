using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {
    [SerializeField] private float defaultDuration = 0.2f;
    [SerializeField] private float defaultMagnitude = 0.15f;

    private Vector3 originalPos;

    private void Awake() {
        originalPos = transform.localPosition;
    }

    public void Shake() {
        StartCoroutine(ShakeRoutine(defaultDuration, defaultMagnitude));
    }

    public void Shake(float duration, float magnitude) {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude) {
        float elapsed = 0f;
        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
