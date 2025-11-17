using UnityEngine;
using System.Collections;

public class BurntIngredientLifetime : MonoBehaviour {
    [SerializeField] private float lifeTime = 3f;  // อยู่ 3 วินาที

    private void OnEnable() {
        StartCoroutine(LifeRoutine());
    }

    private IEnumerator LifeRoutine() {
        yield return new WaitForSeconds(lifeTime);

        // หา Cell ที่ตัวนี้อยู่ข้างใน
        var cell = GetComponentInParent<Cell>();
        if (cell != null) {
            cell.Clear();
        }

        // ทำลายตัวเอง
        Destroy(gameObject);

        // ให้ Board เติมช่องว่าง
        var board = FindObjectOfType<Board>();
        if (board != null) {
            board.RefillEmpty();
        }
    }
}
