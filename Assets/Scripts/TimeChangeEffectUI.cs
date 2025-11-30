using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeChangeEffectUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    [Header("Sprites")]
    [SerializeField] private Sprite plusTimeSprite;   // รูปแมวดีใจ (Golden Milk)
    [SerializeField] private Sprite minusTimeSprite;  // รูปแมวร้องไห้ (Burnt Coffee)

    [Header("Animation")]
    [SerializeField] private float popDuration = 0.12f;
    [SerializeField] private float showDuration = 0.4f;
    [SerializeField] private float settleDuration = 0.1f;

    [Header("Shake")]
    [SerializeField] private float shakeMagnitude = 8f;    // ระยะสั่น (พิกเซล)
    [SerializeField] private float shakeFrequency = 18f;   // ความถี่การสั่น

    private Coroutine currentRoutine;

    private void Awake()
    {
        if (iconImage == null)
            iconImage = GetComponent<Image>();

        if (iconImage != null)
            iconImage.gameObject.SetActive(false);
    }

    public void ShowPlus()
    {
        Show(plusTimeSprite);
    }

    public void ShowMinus()
    {
        Show(minusTimeSprite);
    }

    private void Show(Sprite sprite)
    {
        if (iconImage == null || sprite == null) return;

        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(PlayRoutine(sprite));
    }

    private IEnumerator PlayRoutine(Sprite sprite)
    {
        iconImage.sprite = sprite;
        iconImage.gameObject.SetActive(true);

        RectTransform rt = iconImage.rectTransform;
        Vector2 originalPos = rt.anchoredPosition;   // ตำแหน่งเดิม (สำคัญมาก)
        rt.localScale = Vector3.zero;

        float elapsed = 0f;

        // 1) เด้ง 0 -> 1.15
        while (elapsed < popDuration)
        {
            float t = elapsed / popDuration;
            float s = Mathf.Lerp(0f, 1.15f, t);
            rt.localScale = new Vector3(s, s, 1f);

            // shake เบา ๆ ตอนเด้ง
            float shakeT = Time.time * shakeFrequency;
            Vector2 offset = new Vector2(
                Mathf.Sin(shakeT) * shakeMagnitude * 0.4f,
                Mathf.Cos(shakeT * 1.2f) * shakeMagnitude * 0.4f
            );
            rt.anchoredPosition = originalPos + offset;

            elapsed += Time.deltaTime;
            yield return null;
        }
        rt.localScale = new Vector3(1.15f, 1.15f, 1f);

        // 2) ค้างโชว์ + สั่นดุ๊กดิ๊ก
        elapsed = 0f;
        while (elapsed < showDuration)
        {
            float shakeT = Time.time * shakeFrequency;
            Vector2 offset = new Vector2(
                Mathf.Sin(shakeT) * shakeMagnitude,
                Mathf.Cos(shakeT * 1.3f) * shakeMagnitude
            );
            rt.anchoredPosition = originalPos + offset;

            // เพิ่ม squish scale นิด ๆ ให้ดูนุ่ม
            float squish = 1f + Mathf.Sin(shakeT * 0.8f) * 0.05f;
            rt.localScale = new Vector3(1.1f * squish, 1.1f / squish, 1f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 3) เก็บ scale ลง 1.15 -> 1.0 และคืนตำแหน่ง
        elapsed = 0f;
        while (elapsed < settleDuration)
        {
            float t = elapsed / settleDuration;
            float s = Mathf.Lerp(1.15f, 1.0f, t);
            rt.localScale = new Vector3(s, s, 1f);

            // ลดการสั่นลงเรื่อย ๆ
            float damp = 1f - t;
            float shakeT = Time.time * shakeFrequency;
            Vector2 offset = new Vector2(
                Mathf.Sin(shakeT) * shakeMagnitude * 0.3f * damp,
                Mathf.Cos(shakeT * 1.1f) * shakeMagnitude * 0.3f * damp
            );
            rt.anchoredPosition = originalPos + offset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        rt.localScale = Vector3.one;
        rt.anchoredPosition = originalPos;   // คืนตำแหน่งให้เป๊ะ

        iconImage.gameObject.SetActive(false);
        currentRoutine = null;
    }
}
