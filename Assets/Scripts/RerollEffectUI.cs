using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RerollEffectUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private float popDuration = 0.12f;
    [SerializeField] private float showDuration = 0.35f;
    [SerializeField] private float settleDuration = 0.08f;
    [SerializeField] private float rotateSpeed = 420f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        if (iconImage == null)
            iconImage = GetComponent<Image>();

        // ซ่อนแค่รูป ไม่ต้องปิดทั้ง GameObject
        if (iconImage != null)
            iconImage.gameObject.SetActive(false);
    }

    public void Play()
    {
        if (iconImage == null) return;

        // เผื่อโดนปิดทั้ง GO ไว้ ก็บังคับเปิดก่อน
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        iconImage.gameObject.SetActive(true);

        RectTransform rt = iconImage.rectTransform;
        rt.localScale = Vector3.zero;
        rt.localRotation = Quaternion.identity;

        // 1) เด้ง 0 -> 1.2
        float elapsed = 0f;
        while (elapsed < popDuration)
        {
            float t = elapsed / popDuration;
            float s = Mathf.Lerp(0f, 1.2f, t);
            rt.localScale = new Vector3(s, s, 1f);

            rt.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rt.localScale = new Vector3(1.2f, 1.2f, 1f);

        // 2) หมุนโชว์
        elapsed = 0f;
        while (elapsed < showDuration)
        {
            rt.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 3) เก็บลง 1.2 -> 1.0
        elapsed = 0f;
        while (elapsed < settleDuration)
        {
            float t = elapsed / settleDuration;
            float s = Mathf.Lerp(1.2f, 1.0f, t);
            rt.localScale = new Vector3(s, s, 1f);

            rt.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime * 0.5f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rt.localScale = Vector3.one;
        rt.localRotation = Quaternion.identity;

        iconImage.gameObject.SetActive(false);
        currentRoutine = null;
    }
}
