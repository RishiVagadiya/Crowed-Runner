using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public Button resumeButton;
    public Button RestartButton;
    public Transform Player;
    private Vector3 StartPosition;

    void Start()
    {
        // Show Resume button only if the game was paused before
        if (PlayerPrefs.GetInt("GamePaused", 0) == 1)
        {
            resumeButton.gameObject.SetActive(true);
            RestartButton.gameObject.SetActive(true);
        }
        else
        {
            resumeButton.gameObject.SetActive(false);
            resumeButton.gameObject.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        SceneManager.LoadScene("Game"); // Load Game Scene
    }

    public void RestartGame()
    {
        Player.position = StartPosition;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
