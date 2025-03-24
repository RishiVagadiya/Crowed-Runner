using UnityEngine;
using TMPro;

public class AsksQuestions : MonoBehaviour
{
    public GameObject questionPanel; // Assign the UI Panel in Inspector
    public TextMeshProUGUI questionText; // Assign the TextMeshPro Text component in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player touches the question panel
        {
            ShowQuestion();
        }
        else if (other.CompareTag("CorrectAnswer") || other.CompareTag("WrongAnswer")) // Check for "4" or "2" panels
        { 
            questionPanel.gameObject.SetActive(false);
            questionText.gameObject.SetActive(false);
            DisableQPanle();
        }
    }

    void ShowQuestion()
    {
        questionPanel.SetActive(true); // Show the UI panel
    }

    void DisableQPanle()
    {
        questionText.gameObject.SetActive(false);
    }
}
