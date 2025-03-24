using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class NewGameReset : MonoBehaviour
{
    private string saveFilePath;

    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/saveData.json"; // JSON फाइल का रास्ता
    }

    public void ResetGame()
    {
        // अगर JSON फाइल मौजूद है, तो उसे डिलीट कर दो
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }

        // PlayerPrefs भी डिलीट करें अगर कुछ स्टोर किया है
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // लेवल 1 (या पहला सीन) लोड करो
        SceneManager.LoadScene("GAME");
    }
}
