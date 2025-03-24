using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;  // Assign Player in Inspector
    public GameObject homeButton; // Assign Home Button in Inspector

    void Start()
    {
        // If coming from Resume, restore the last saved position
        if (PlayerPrefs.GetInt("GamePaused", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX", 0);
            float y = PlayerPrefs.GetFloat("PlayerPosY", 0);
            float z = PlayerPrefs.GetFloat("PlayerPosZ", 0);
            player.transform.position = new Vector3(x, y, z);

            Time.timeScale = 1; // Resume the game
            PlayerPrefs.SetInt("GamePaused", 0); // Reset Pause flag
        }

        // Enable Home button at the start of the level
        homeButton.SetActive(true);
    }

    public void GoToHome()
    {
        // Save player's current position
        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);

        PlayerPrefs.SetInt("GamePaused", 1); // Set pause flag
        Time.timeScale = 0; // Pause the game
        SceneManager.LoadScene("HomeMenu"); // Load Home Scene
    }
}
