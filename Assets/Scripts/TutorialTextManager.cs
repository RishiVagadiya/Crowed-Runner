using UnityEngine;
using TMPro;

public class TutorialTextManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // UI Text ka reference
    public bool isTutorialMode = false;  // Tutorial mode check

    void Start()
    {
        ShowStartMessage(); // Game start hone par message show karega
    }

    void ShowStartMessage()
    {
        if (isTutorialMode)
        {
            tutorialText.text = "Touch & Hold <- -> For Move";
        }
        
        tutorialText.gameObject.SetActive(true);
        Invoke("HideText", 6f); // 3 sec ke baad text hide hoga
    }

    void HideText()
    {
        tutorialText.gameObject.SetActive(false);
    }
}
