using UnityEngine;

public class ScoreDebugger : MonoBehaviour
{
    void Start()
    {
        ScoreManager[] scoreScripts = FindObjectsOfType<ScoreManager>(); 
        Debug.Log("Total ScoreManager Scripts Found: " + scoreScripts.Length);

        foreach (ScoreManager script in scoreScripts)
        {
            Debug.Log("Score Script Found on: " + script.gameObject.name);
        }
    }
}
