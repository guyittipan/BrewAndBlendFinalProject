using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;   // 👈 เพิ่ม using นี้สำหรับ Coroutine

public class MixResultUI : MonoBehaviour
{
    [System.Serializable]
    public class RecipeResult
    {
        public string recipeName; // ชื่อสูตรจาก RecipeDatabase
        public Sprite resultSprite; // รูปที่จะโชว์
    }

    [Header("UI")]
    [SerializeField] private Image resultImage;  // รูปตรงกลางจอ
    [SerializeField] private float showTime = 1.5f;

    [Header("Recipe Results")]
    [SerializeField] private List<RecipeResult> recipeResults = new List<RecipeResult>();

    private Dictionary<string, Sprite> recipeDict = new Dictionary<string, Sprite>();
    private float timer;
    private bool isShowing;

    private Coroutine popRoutine;   // 👈 เก็บ coroutine ของเอฟเฟกต์เด้ง

    void Awake()
    {
        foreach (var r in recipeResults)
        {
            if (!recipeDict.ContainsKey(r.recipeName))
                recipeDict.Add(r.recipeName, r.resultSprite);
        }

        if (resultImage != null)
            resultImage.gameObject.SetActive(false);
    }

    public void Show(string recipeName)
    {
        if (resultImage == null) return;
        if (!recipeDict.ContainsKey(recipeName)) return;

        resultImage.sprite = recipeDict[recipeName];
        resultImage.gameObject.SetActive(true);

        isShowing = true;
        timer = showTime;

        // 👇 เริ่มเอฟเฟกต์เด้งใหม่ทุกครั้งที่โชว์
        if (popRoutine != null)
            StopCoroutine(popRoutine);
        popRoutine = StartCoroutine(PopAnimation());
    }

    void Update()
    {
        if (!isShowing) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            resultImage.gameObject.SetActive(false);
            isShowing = false;
        }
    }

    // 👇 เอฟเฟกต์เด้งเล็ก ๆ ตอนรูปโผล่
    private IEnumerator PopAnimation()
{
    RectTransform rt = resultImage.rectTransform;
    rt.localScale = Vector3.zero;

    // เฟสเด้งขึ้น เร็วขึ้น (0.12 วินาที)
    float duration = 0.12f;
    float elapsed = 0f;

    while (elapsed < duration)
    {
        float t = elapsed / duration;
        float scale = Mathf.Lerp(0f, 1.15f, t);  // ขยายขึ้นเร็วกว่าเดิมนิดนึง
        rt.localScale = new Vector3(scale, scale, 1f);

        elapsed += Time.deltaTime;
        yield return null;
    }

    // เฟสคืนสู่ขนาดปกติ เร็วขึ้น (0.08 วินาที)
    duration = 0.08f;
    elapsed = 0f;
    while (elapsed < duration)
    {
        float t = elapsed / duration;
        float scale = Mathf.Lerp(1.15f, 1.0f, t);
        rt.localScale = new Vector3(scale, scale, 1f);

        elapsed += Time.deltaTime;
        yield return null;
    }

    rt.localScale = Vector3.one;
}
}
