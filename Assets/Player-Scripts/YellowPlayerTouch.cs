using UnityEngine;
using TMPro;

public class YellowPlayerTouch : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip touchSound;
    public ParticleSystem touchEffect;
    public Transform touchEffectSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("YellowPlayer"))
        {
            // Play the sound if assigned
            if (audioSource != null && touchSound != null)
            {
                audioSource.PlayOneShot(touchSound);
            }

            // Play particle effect if assigned
            if (touchEffect != null && touchEffectSpawnPoint != null)
            {
                Instantiate(touchEffect, touchEffectSpawnPoint.position, Quaternion.identity);
            }

            // Add 10 points when the red player is touched
            ScoreManager.instance.AddScore(10);

            // Destroy the yellow player after touch
            Destroy(other.gameObject);
        }
    }
}
