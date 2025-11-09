
using UnityEngine;

public class IngredientSpawner : MonoBehaviour {
    [SerializeField] private Ingredient[] ingredientPrefabs; // prefabs to spawn
    [SerializeField] private float[] spawnWeights; // optional weights, same length as ingredientPrefabs

    public Ingredient GetRandomIngredientInstance(Transform parent = null) {
        var prefab = GetRandomPrefab();
        if (prefab == null) return null;
        var go = GameObject.Instantiate(prefab, parent);
        return go;
    }

    private Ingredient GetRandomPrefab() {
        if (ingredientPrefabs == null || ingredientPrefabs.Length == 0) return null;
        if (spawnWeights != null && spawnWeights.Length == ingredientPrefabs.Length) {
            float total = 0f;
            foreach (var w in spawnWeights) total += w;
            float r = Random.value * total;
            float accum = 0f;
            for (int i = 0; i < ingredientPrefabs.Length; i++) {
                accum += spawnWeights[i];
                if (r <= accum) return ingredientPrefabs[i];
            }
            return ingredientPrefabs[ingredientPrefabs.Length - 1];
        } else {
            int idx = Random.Range(0, ingredientPrefabs.Length);
            return ingredientPrefabs[idx];
        }
    }
}
