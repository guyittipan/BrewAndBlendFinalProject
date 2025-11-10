using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    void Awake()
    {
        // ถ้ามี BGMManager อยู่แล้วในซีนอื่น อย่าสร้างซ้ำ
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // ให้เล่นต่อทุกซีน
    }
}
