using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Ensure Player has the tag "Player"
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Die();  // Call the Die() function from PlayerMovement script
            }
        }
    }
}
