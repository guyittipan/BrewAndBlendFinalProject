
using UnityEngine;
using UnityEngine.UI;

public class EntranceSceneController : MonoBehaviour {
    [SerializeField] private Button startButton;
    [SerializeField] private GameController gameController;

    private void Start() {
        if (startButton != null) startButton.onClick.AddListener(OnStart);
    }

    private void OnStart() {
        gameController?.StartGameplay();
    }
}
