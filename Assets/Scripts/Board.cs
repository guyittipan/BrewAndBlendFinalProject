
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour {
    [SerializeField] private Cell[] cells = new Cell[9];
    [SerializeField] private IngredientSpawner spawner;

    private void Start() {
        if (spawner == null) Debug.LogWarning("IngredientSpawner not assigned to Board.");
        RefillAll();
    }

    public Cell GetCell(int index) {
        if (index < 0 || index >= cells.Length) return null;
        return cells[index];
    }

    public Cell[] GetAllCells() => cells;

    public void RefillEmpty() {
        if (spawner == null) return;
        for (int i = 0; i < cells.Length; i++) {
            if (cells[i] != null && cells[i].IsEmpty()) {
                var ing = spawner.GetRandomIngredientInstance(transform);
                cells[i].SetIngredient(ing);
            }
        }
    }

    public void RefillAll() {
        if (spawner == null) return;
        for (int i = 0; i < cells.Length; i++) {
            if (cells[i] != null) {
                var ing = spawner.GetRandomIngredientInstance(transform);
                cells[i].SetIngredient(ing);
            }
        }
    }
    // เรียกใช้จาก Editor script หรือเมนู context
    public void AutoAssignCellsFromChildren()
    {
        var childCells = GetComponentsInChildren<Cell>(true);
        // sort by hierarchy order (already returned in hierarchy order)
        int count = Mathf.Min(childCells.Length, 9);
        cells = new Cell[9];
        for (int i = 0; i < count; i++)
        {
            cells[i] = childCells[i];
            childCells[i].gameObject.name = "Cell_" + i;
        }
        // remaining slots null
        for (int i = count; i < 9; i++) cells[i] = null;
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
}
