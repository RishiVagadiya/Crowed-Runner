using UnityEngine;
using TMPro;

public class SecondQuestion : MonoBehaviour
{
    public GameObject questionPanel; // Assign the UI Panel in Inspector
    public GameObject AfterTouchedPanel;
    public TextMeshProUGUI questionText; // Assign the TextMeshPro Text component in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player touches the question panel
        {
            ShowQuestion();
        }
        else if (other.CompareTag("CorrectAnswer") || other.CompareTag("WrongAnswer")) // Check for "4" or "2" panels
        {
            // Disable only the question text
            questionText.gameObject.SetActive(false);
        }
    }

    void ShowQuestion()
    {
        questionPanel.SetActive(true); // Show the UI panel
        questionText.text = "What is 5 x 2?"; // Set your question here
        AfterTouchedPanel.SetActive(false);
    }
}
