using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disablehomebutton : MonoBehaviour
{
    public GameObject homeButton; // Assign Home Button in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the Player has the tag "Player"
        {
            homeButton.SetActive(false); // Disable Home button when player touches finish line
        }
    }
}
