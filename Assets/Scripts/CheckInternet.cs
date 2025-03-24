using UnityEngine;
using UnityEngine.UI;

public class CheckInternet : MonoBehaviour
{
    public GameObject InternetPanel;   // इंटरनेट पैनल
    public Image wifiIcon;             // WiFi Icon
    public Sprite wifiGreen, wifiRed;  // WiFi के Sprites

    private BackgroundMusicController musicController;

    void Start()
    {
        InvokeRepeating("CheckConnection", 0f, 5f); // हर 5 सेकंड में इंटरनेट चेक होगा
          Debug.Log("Checking Platform...");
    if (Application.platform == RuntimePlatform.Android) {
        Debug.Log("Running on Android");
    } else {
        Debug.Log("Not Running on Android, Current Platform: " + Application.platform);
    }
    }

    void CheckConnection()
    {
        //Debug.Log("Checking Internet Connection...");
        
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("No Internet! Showing Panel...");
            wifiIcon.sprite = wifiRed; 
            InternetPanel.SetActive(true);
            
            if (musicController != null)
            {
                musicController.StopMusic();
            }
        }
        else
        {
            Debug.Log("Internet Available! Hiding Panel...");
            wifiIcon.sprite = wifiGreen; 
            InternetPanel.SetActive(false);
            
            if (musicController != null)
            {
                musicController.PlayMusic();
            }
        }
    }

    // Retry बटन पर यह Method Call होगा
    public void RetryConnection()
    {
        Debug.Log("Retrying Connection...");
        CheckConnection();
        Invoke("ForceDisablePanel", 1f); // 1 सेकंड बाद पैनल को फोर्सफुली हटाएगा
    }

    // Panel को Forcefully Disable करने के लिए Extra Safety
    void ForceDisablePanel()
    {
         Debug.Log("Hiding Internet Panel on: " + Application.platform);

    #if UNITY_ANDROID
        Debug.Log("Running on Android");
    #endif

    InternetPanel.SetActive(false);

    }
}
