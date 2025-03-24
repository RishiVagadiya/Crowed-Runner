using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerPanel : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Assign the TMP object in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            questionText.gameObject.SetActive(false); // Disable the question text
        }
    }
}
