using UnityEngine;
using System.Collections;

public class ThrowAway : MonoBehaviour
{
    public float throwForce = 20f; // Throw force
    public GameObject level3; // Level 3 reference
    public Transform player; // Player Transform
    public Animator playerAnimator; // Player Animator (अब Public है)
    
    private PlayerMovement playerMovement; // Player Movement Script
    private Vector3 savedPlayerPosition; // Saved Position

    private void Start()
    {
        // Save player position at start
        if (player != null)
        {
            savedPlayerPosition = player.position;
            playerMovement = player.GetComponent<PlayerMovement>(); // Get Player Movement script
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // If player touches RightTrigger or LeftTrigger
        {
            Debug.Log("Player touched " + gameObject.name);

            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null && playerMovement != null && playerAnimator != null)
            {
                // 1️⃣ Stop Player Movement
                playerMovement.enabled = false;
                rb.velocity = Vector3.zero;

                // 2️⃣ Play Throw & Falling Animation
                playerAnimator.SetTrigger("Throw");
                playerAnimator.SetTrigger("Falling");

                // 3️⃣ Determine Throw Direction
                Vector3 throwDirection = CompareTag("RightTrigger") ? Vector3.right : Vector3.left;
                Debug.Log($"Throwing {throwDirection} with force {throwForce}");

                // 4️⃣ Apply Throw Force
                rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

                // 5️⃣ Restart Level & Load Saved Position after 3 seconds
                StartCoroutine(RestartLevelWithDelay(3f));
            }
        }
    }

    private IEnumerator RestartLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (player != null)
        {
            // 6️⃣ Reset Player Position
            player.position = savedPlayerPosition;
            Debug.Log("✅ Player position loaded after " + delay + " seconds: " + savedPlayerPosition);

            // 7️⃣ Enable Player Movement & Start Running Animation
            if (playerMovement != null && playerAnimator != null)
            {
                playerMovement.enabled = true;
                playerAnimator.SetTrigger("Run"); // 🔥 Running Animation Trigger
                
                Debug.Log("✅ Player movement restarted with Running animation!");
            }
        }
    }
}
