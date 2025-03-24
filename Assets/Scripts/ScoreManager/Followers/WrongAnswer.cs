using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongAnswer : MonoBehaviour
{
    public AudioClip correctSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            ScoreManager.instance.AddScore(-10);
             if (correctSound != null && audioSource != null)
             {
                audioSource.PlayOneShot(correctSound);
             }
        }
    }
}
