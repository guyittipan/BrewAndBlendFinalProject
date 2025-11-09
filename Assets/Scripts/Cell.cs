
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    [SerializeField] private Ingredient currentIngredient;
    [SerializeField] private Button button;
    [SerializeField] private MixSystem mixSystem;
    [SerializeField] private Image ingredientImage; // optional UI image to show sprite

    public Ingredient CurrentIngredient => currentIngredient;

    private void Start() {
        if (button != null) button.onClick.AddListener(OnClick);
    }

    public void SetIngredient(Ingredient ingredient) {
        currentIngredient = ingredient;
        if (ingredientImage != null && ingredient != null)
        {
            Image ingImg = ingredient.GetComponent<Image>();
            if (ingImg != null)
            {
                ingredientImage.sprite = ingImg.sprite; // ดึง sprite จาก prefab ที่เราเซ็ตไว้
                ingredientImage.enabled = true;
            }
        }
    }

    private void OnClick() {
        if (mixSystem != null) mixSystem.SelectCell(this);
    }

    public void Clear() {
        currentIngredient = null;
    }

    public bool IsEmpty() {
        return currentIngredient == null;
    }

}
