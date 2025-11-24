using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float defaultDuration = 0.2f;
    [SerializeField] private float defaultMagnitude = 0.15f;

    private Vector3 originalPos;
    private Quaternion originalRot;

    private void Awake()
    {
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;
    }

    // ================== เขย่ากล้อง ==================
    public void Shake()
    {
        StartCoroutine(ShakeRoutine(defaultDuration, defaultMagnitude));
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    // ================== หมุนกล้องหนึ่งรอบ ==================
    public void SpinOnce(float duration = 0.6f)
    {
        StartCoroutine(SpinRoutine(duration));
    }

    private IEnumerator SpinRoutine(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;             // 0 → 1
            float angle = Mathf.Lerp(0f, 360f, t);   // หมุนครบหนึ่งรอบ
            transform.localRotation = originalRot * Quaternion.Euler(0f, 0f, angle);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // คืนกลับ
        transform.localRotation = originalRot;
    }
}
