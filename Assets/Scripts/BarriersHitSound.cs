using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarriersHitSound : MonoBehaviour
{
      public AudioSource hitSound; // Assign the AudioSource in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that touched the yellow line is the Player
        if (other.CompareTag("Player"))
        {
            // Play the hit sound
            if (hitSound != null)
            {
                hitSound.Play();
            }

            // Reset the score to 0 using ScoreManager
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.ResetScoreOnNewLevel();
            }

            Debug.Log("Player touched the yellow line! Score reset to 0.");
        }
    }
}

