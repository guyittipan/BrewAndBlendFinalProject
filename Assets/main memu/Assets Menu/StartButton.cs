using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioSource clickSound; // ช่องใส่เสียงคลิก

    public void StartGame()
    {
        StartCoroutine(PlayClickAndStart());
    }

    private IEnumerator PlayClickAndStart()
    {
        // ถ้ามีเสียงคลิกให้เล่นก่อน
        if (clickSound != null)
        {
            clickSound.Play();
            yield return new WaitForSeconds(0.3f); // หน่วงเวลา 0.3 วินาทีให้เสียงดังทัน
        }

        // โหลดซีน Gameplay
        SceneManager.LoadScene("Gameplay");
    }
      public void GoToMainMenu()
    {
        StartCoroutine(PlayClickAndGoToMainMenu());
    }

    private IEnumerator PlayClickAndGoToMainMenu()
    {
        if (clickSound != null)
        {
            clickSound.Play();
            yield return new WaitForSeconds(0.3f);
        }

        SceneManager.LoadScene("Entrance");
    }
}

