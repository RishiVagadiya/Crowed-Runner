using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correctanswer : MonoBehaviour
{
    public AudioClip correctSound;
    private AudioSource audioSource;
    private List<GameObject> disabledBluePlayers = new List<GameObject>(); // Disable हुए players को स्टोर करने के लिए लिस्ट

    public int panelNumber; // **Inspector में Set करने के लिए Panel Number**

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Player का Tag चेक करें
        {
            ScoreManager.instance.AddScore(10);

            // Sound Play करें
            if (correctSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(correctSound);
            }

            // **Panel के number के अनुसार players को enable करना**
            EnableBluePlayers(panelNumber);
        }
    }

    public void DisableBluePlayer(GameObject player)  // जब player disable हो तो उसे लिस्ट में add करें
    {
        if (!disabledBluePlayers.Contains(player))
    {
        disabledBluePlayers.Add(player);
        player.SetActive(false);
        Debug.Log("Player Disabled and Added to List: " + player.name);
    }
    }

    void EnableBluePlayers(int count)
    {
    int reEnabledCount = 0;
    
    for (int i = 0; i < disabledBluePlayers.Count && reEnabledCount < count; i++)
    {
        if (!disabledBluePlayers[i].activeSelf)
        {
            disabledBluePlayers[i].SetActive(true);
            reEnabledCount++;
        }
    }

    Debug.Log("Re-enabled Players: " + reEnabledCount);
}
}
