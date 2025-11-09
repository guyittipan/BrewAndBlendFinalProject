
using UnityEngine;
using UnityEngine.UI;

public class TutorialSceneController : MonoBehaviour {
    [SerializeField] private Button playButton;
    [SerializeField] private GameController gameController;

    private void Start() {
        if (playButton != null) playButton.onClick.AddListener(OnPlay);
    }

    private void OnPlay() {
        gameController?.StartGameplay();
    }
}
