using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leveltwo : MonoBehaviour
{
   public static Leveltwo instance;  
    public int currentScore = 0;          
    public Text scoreText;     


    private void Start()
    {
        UpdateScoreText(); 
    }
    private void Awake()
    {
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

    
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreText();
    }

    
    public void ResetScoreOnNewLevel()
    {
        currentScore = 0;
        UpdateScoreText();
    }

    
    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString(); 
        }
    }
}
