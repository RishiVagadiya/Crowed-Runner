using UnityEngine;
using TMPro;
public class Level2QuePanel : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI questionText; 

    private void Start()
    {
        if (questionText != null)
            questionText.gameObject.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (questionText != null)
                questionText.gameObject.SetActive(true); 
        }
    }

    public static void ResetAllQuestions()
    {
       
        QuestionTrigger[] allQuestions = FindObjectsOfType<QuestionTrigger>();
        foreach (QuestionTrigger q in allQuestions)
        {
            if (q.questionText != null)
                q.questionText.gameObject.SetActive(false);
        }
    }

   
    public void OnRetryButtonClick()
    {
        ResetAllQuestions();     
    }
}
