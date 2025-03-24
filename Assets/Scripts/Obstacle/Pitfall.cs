using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;  // Scene Reload करने के लिए

public class Pitfall : MonoBehaviour
{
    public AudioClip fallSound;  // गिरने की आवाज़
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if Player falls
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                PlayFallSound();  // गिरने की आवाज़ बजाओ
                player.Die();      // Die Animation चलाओ
                StartCoroutine(RestartLevel()); // 3 सेकंड बाद Restart करो
            }
        }
    }

    void PlayFallSound()
    {
        if (fallSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fallSound);
        }
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3f);  // 3 सेकंड wait करो
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Level Restart करो
    }
}
