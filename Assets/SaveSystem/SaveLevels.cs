using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public int savedLevel;
}

public class SaveLevels : MonoBehaviour
{
  public GameObject[] levels; // Sabhi levels ka reference
    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/gameData.json";
        Debug.Log("Save Path: " + savePath);  // Debugging के लिए

        if (IsFirstTimeLaunch())
        {
            ResetProgress(); // Pehli baar install hone par reset karega
        }

        int savedLevel = LoadGame(); // Saved Level load karo
        ActivateLevel(savedLevel);   // Usi level ko activate karo
    }

    public void SaveGame(int levelIndex)
    {
        GameData data = new GameData();
        data.savedLevel = levelIndex; // Sirf completed level save hoga

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved! Next Start Level: " + levelIndex);
    }

    private int LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            return data.savedLevel;
        }
        return 0; // Default Level 1 (Index 0)
    }

    private void ActivateLevel(int levelIndex)
    {
        Debug.Log("Activating Level: " + levelIndex);

        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i] != null)
                levels[i].SetActive(i == levelIndex); // Sirf saved level active hoga
        }
    }

    private bool IsFirstTimeLaunch()
    {
        return !File.Exists(savePath);
    }

    public void ResetProgress()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        Debug.Log("Game Progress Reset! Starting from Level 1.");
    }
}
