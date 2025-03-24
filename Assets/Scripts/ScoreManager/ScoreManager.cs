using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;  // Singleton instance
    public int currentScore = 0;          // The current score
    public Text scoreText;     // UI text for displaying score


    private void Start()
    {
        UpdateScoreText();  // 🟢 Level स्टार्ट होते ही Score UI अपडेट होगा
    }
    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        UpdateScoreText();  
    }

    // Method to add points
    public void AddScore(int points)
    {
    currentScore += points;
    UpdateScoreText();
    }

    // Method to reset score when new level starts
    public void ResetScoreOnNewLevel()
    {
        currentScore = 0;
        UpdateScoreText();
    }

    // Update the UI text
    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString(); 
        }
    }
}
