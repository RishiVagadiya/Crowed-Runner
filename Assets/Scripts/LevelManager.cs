using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
   
    public void LoadNextLevel()
    {
        ScoreManager.instance.UpdateScoreText();

        level1.SetActive(false); // Disable Level 1
        level2.SetActive(true); // Enable Level 2
    }
     

}
