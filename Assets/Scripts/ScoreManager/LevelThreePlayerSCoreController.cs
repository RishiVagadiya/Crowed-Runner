using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreePlayerSCoreController : MonoBehaviour
{
    public AudioClip correctSound;
    public AudioClip wrongSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("CorrectAnswer"))
    {
        // Add 10 points for a correct answer
        ThreelevelScore.instance.AddScore(10);

        // Play correct sound
        if (correctSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(correctSound);
        }
    }
    else if (other.CompareTag("WrongAnswer"))
    {
        // Deduct 10 points for a wrong answer
        ThreelevelScore.instance.AddScore(-10);

        // Play wrong sound
        if (wrongSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(wrongSound);
        }
    }
}

}
