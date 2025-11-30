using UnityEngine;

public class LogoIdleBounce : MonoBehaviour
{
    [Header("Move (เด้งขึ้นลง)")]
    [SerializeField] private float moveAmplitude = 15f;   // ระยะเด้ง (พิกเซล)
    [SerializeField] private float moveSpeed = 1.5f;      // ความเร็วการเด้ง

    [Header("Scale (ยุบๆ พองๆ)")]
    [SerializeField] private float scaleAmplitude = 0.03f; // ขนาดเด้ง (เช่น 0.03 = +/-3%)
    [SerializeField] private float scaleSpeed = 2f;        // ความเร็วการยุบพอง

    private RectTransform rt;
    private Vector2 startPos;
    private Vector3 startScale;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        if (rt != null)
        {
            startPos = rt.anchoredPosition;
            startScale = rt.localScale;
        }
    }

    private void Update()
    {
        if (rt == null) return;

        float t = Time.time;

        // เด้งขึ้นลง
        float offsetY = Mathf.Sin(t * moveSpeed) * moveAmplitude;
        rt.anchoredPosition = startPos + new Vector2(0f, offsetY);

        // ยุบๆ พองๆ
        float scaleFactor = 1f + Mathf.Sin(t * scaleSpeed) * scaleAmplitude;
        rt.localScale = startScale * scaleFactor;
    }

    // กันไว้เผื่อถูกปิด/เปลี่ยนซีน จะได้กลับสภาพปกติ
    private void OnDisable()
    {
        if (rt != null)
        {
            rt.anchoredPosition = startPos;
            rt.localScale = startScale;
        }
    }
}
