using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (!recipeDict.ContainsKey(recipeName)) return;

        resultImage.sprite = recipeDict[recipeName];
        resultImage.gameObject.SetActive(true);
        isShowing = true;
        timer = showTime;
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
}
