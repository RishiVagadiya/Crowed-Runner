using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victoryanimation : MonoBehaviour
{
     public Animator playerAnimator; // Assign this in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player reached the finish line
        {
            // If no animator is assigned, try getting it from the player
            if (playerAnimator == null)
            {
                playerAnimator = other.GetComponent<Animator>();
            }

            // If the player still has no Animator, log a warning
            if (playerAnimator == null)
            {
                Debug.LogWarning("Player has no Animator component assigned!");
                return;
            }

            // Play the victory animation
            playerAnimator.SetTrigger("Victory");
        }
    }
}
