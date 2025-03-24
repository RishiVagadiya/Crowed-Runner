using UnityEngine;

public class QuestionDisabler : MonoBehaviour
{
    public GameObject questionText; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            questionText.SetActive(false); 
        }
    }
}
