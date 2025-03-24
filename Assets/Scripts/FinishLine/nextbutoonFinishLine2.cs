using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextbutoonFinishLine2 : MonoBehaviour
{
    public GameObject nextButton; // Assign in Inspector

    private void Start()
    {
        nextButton.SetActive(false); // Hide the button at the start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // If the player reaches the finish line
        {
            nextButton.SetActive(true); // Show the Next Level button
        }
    }
}
