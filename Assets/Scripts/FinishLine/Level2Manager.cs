using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
     public GameObject level2;
    public GameObject level3;
    public Transform player;  // Player का Transform 
    public Vector3 level2StartPosition; // Level 2 की Starting Position
    private Rigidbody playerRb; // Player का Rigidbody

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>(); // Player का Rigidbody Component लें
    }

    public void RetryLevel()
    {
        Debug.Log("Retry Button Clicked - Restarting Level 2");

        // सिर्फ Level 2 को Active करें
        level2.SetActive(true);
        level3.SetActive(false);  // Level 3 को disable करें

        // Player की Position Reset करें
        ResetPlayerPosition();

        // Score और अन्य ज़रूरी चीज़ें Reset करें
        Leveltwo.instance.ResetScoreOnNewLevel();
        InitializeLevel2();
    }

    private void ResetPlayerPosition()
    {
        player.position = level2StartPosition;  // Player को Level 2 की Start Position पर भेजें

        // Rigidbody की Velocity Reset करें ताकि हवा में न रहे
        if (playerRb != null)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        Debug.Log("Player Position Reset to: " + level2StartPosition);
    }

    private void InitializeLevel2()
    {
        Debug.Log("Initializing Level 2...");
        // यहाँ Level 2 की ज़रूरी चीज़ें Reset करें, जैसे Enemies, Collectibles आदि
    }
}
