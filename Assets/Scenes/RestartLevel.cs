using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene("GAME"); // Reload main game scene
    }
}
