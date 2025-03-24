using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public Animator animator; // Reference to the Animator
    public PlayerMovement playerMovement; // Reference to PlayerMovement script

    private bool isDead = false; // Prevent multiple triggers

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Auto-assign animator if not set
        }

        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>(); // Auto-assign PlayerMovement script
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.CompareTag("Player"))
        {
            isDead = true;
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die"); // Play death animation

        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disable PlayerMovement script to stop movement
        }

        Invoke("ReloadScene", 3f); // Wait for 2 seconds before reloading
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
