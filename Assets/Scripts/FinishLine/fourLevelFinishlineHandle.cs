using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class fourLevelFinishlineHandle : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform playerStartPosition;
    private GameObject player;
    public GameObject finishPanel;
    public GameObject Level4;
    public GameObject Level5;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI levelCompleteText;
    public Button retryButton;
    public Button nextLevelButton;

    [Header("Game Settings")]
    public GameObject[] levels;
    public int currentLevel = 3;

    [Header("Audio & Animation")]
    public Animator animator;
    public string victoryAnimationName = "Victory";
    public string defeatAnimationName = "Defeat";
    public AudioSource victorySound;
    public AudioSource defeatSound;

    [Header("Camera Animation")]
    public Camera mainCamera;
    public Transform cameraTarget;
    public float cameraMoveSpeed = 2f;
    private BackgroundMusicController musicController;
    private const int RequiredScore = 90;
    private Animator playerAnimator;
    private bool cameraMoved = false;

    private void Start()
    { 
        FourLevelScore.instance.ResetScoreOnNewLevel();
        Debug.Log("Score is Reset -> 0");

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>();

            PlayerPrefs.SetFloat("SavedPlayerX", player.transform.position.x);
            PlayerPrefs.SetFloat("SavedPlayerY", player.transform.position.y);
            PlayerPrefs.SetFloat("SavedPlayerZ", player.transform.position.z);

            PlayerPrefs.SetFloat("SavedCameraX", mainCamera.transform.position.x);
            PlayerPrefs.SetFloat("SavedCameraY", mainCamera.transform.position.y);
            PlayerPrefs.SetFloat("SavedCameraZ", mainCamera.transform.position.z);

            PlayerPrefs.SetFloat("SavedCameraRotX", mainCamera.transform.rotation.eulerAngles.x);
            PlayerPrefs.SetFloat("SavedCameraRotY", mainCamera.transform.rotation.eulerAngles.y);
            PlayerPrefs.SetFloat("SavedCameraRotZ", mainCamera.transform.rotation.eulerAngles.z);

            PlayerPrefs.Save();
            Debug.Log("Player & Camera Initial Position and Rotation Saved");
        }

        if (currentLevel == 0)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i].activeSelf)
                {
                    currentLevel = i;
                    break;
                }
            }
        }

        Debug.Log("Final Current Level: " + currentLevel);
        InitializeGame();
    }
     void Update() 
     { 
          if (Input.GetMouseButtonDown(0)) 
         { 
             Debug.Log("Mouse Clicked at: " + Input.mousePosition);
         }
         if (Input.GetMouseButtonDown(0)) 
    { 
        PointerEventData pointerData = new PointerEventData(EventSystem.current) 
        { 
            position = Input.mousePosition 
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results) 
        { 
            Debug.Log("Raycast hit: " + result.gameObject.name);
        }
    }
    }


    private void InitializeGame()
    {
        finishPanel.SetActive(false);
        musicController = FindObjectOfType<BackgroundMusicController>();

        retryButton.onClick.AddListener(RetryGame);
        nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(HandleLevelCompletion(other.transform, other.GetComponent<Animator>()));
        }
    }

    private IEnumerator HandleLevelCompletion(Transform player, Animator playerAnimator)
    {
        int playerScore = GetPlayerScore();

        if (!cameraMoved)
        {
            cameraMoved = true;
            yield return StartCoroutine(MoveCameraToPlayer(player));
        }

        finishPanel.SetActive(true);

        if (playerScore >= RequiredScore)
        {
            levelCompleteText.text = "LEVEL COMPLETE 😊";
            levelCompleteText.color = Color.green;
            nextLevelButton.interactable = true;
            PlayVictoryEffects(playerAnimator);
        }
        else
        {
            levelCompleteText.text = "DEFEAT! Score at least 90!";
            levelCompleteText.color = Color.red;
            nextLevelButton.interactable = false;
            PlayDefeatEffects(playerAnimator);
        }

        finalScoreText.text = "Final Score: " + playerScore;
    }

    private IEnumerator MoveCameraToPlayer(Transform player)
    {
        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;
        Vector3 targetPos = cameraTarget.position;
        Quaternion targetRot = Quaternion.LookRotation(player.position - cameraTarget.position);

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * cameraMoveSpeed;
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime);
            mainCamera.transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsedTime);
            yield return null;
        }
    }

    private int GetPlayerScore()
    {
        return FourLevelScore.instance != null ? FourLevelScore.instance.currentScore : 0;
    }

    private void PlayVictoryEffects(Animator playerAnimator)
    {
        animator.SetTrigger("Victory");
        playerAnimator?.SetTrigger("Victory");
        victorySound?.Play();
        musicController?.StopMusic();
    }

    private void PlayDefeatEffects(Animator playerAnimator)
    {
        Debug.Log("Defeat Triggered!");

        if (playerAnimator != null)
        {
            Debug.Log("Playing Defeat Animation");
            playerAnimator.SetTrigger("Defeat");
        }
        else
        {
            Debug.LogError("Player Animator is NULL!");
        }

        if (defeatSound != null)
        {
            Debug.Log("Playing Defeat Sound");
            defeatSound.Play();
        }
        else
        {
            Debug.LogError("Defeat Sound is NULL!");
        }

        musicController?.StopMusic();
    }

    public void RetryGame()
    {
        Debug.Log("Retry button Clicked!");

         // पहले एड दिखाने का प्रयास करें
    AdManager adManager = FindObjectOfType<AdManager>();
    if (adManager != null)
    {
        adManager.ShowInterstitialAd();
    }
    else
    {
        Debug.LogError("AdManager not found in the scene.");
    }

        if (Level4.activeSelf)
        {
            Level4.SetActive(false);
            Level4.SetActive(true);
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerAnimator = player.GetComponent<Animator>();
        }

        if (player != null)
        {
            float px = PlayerPrefs.GetFloat("SavedPlayerX");
            float py = PlayerPrefs.GetFloat("SavedPlayerY");
            float pz = PlayerPrefs.GetFloat("SavedPlayerZ");

            player.transform.position = new Vector3(px, py, pz);
            Debug.Log("Player Position Loaded on Retry: " + player.transform.position);

            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("isRunning");
                Debug.Log("Running Animation Started!");
            }
        }

        if (mainCamera != null)
        {
            float cx = PlayerPrefs.GetFloat("SavedCameraX");
            float cy = PlayerPrefs.GetFloat("SavedCameraY");
            float cz = PlayerPrefs.GetFloat("SavedCameraZ");

            float crx = PlayerPrefs.GetFloat("SavedCameraRotX");
            float cry = PlayerPrefs.GetFloat("SavedCameraRotY");
            float crz = PlayerPrefs.GetFloat("SavedCameraRotZ");

            mainCamera.transform.position = new Vector3(cx, cy, cz);
            mainCamera.transform.rotation = Quaternion.Euler(crx, cry, crz);

            Debug.Log("Camera Position & Rotation Loaded on Retry");
        }

        if (FourLevelScore.instance != null)
        {
        FourLevelScore.instance.ResetScoreOnNewLevel();
        }

        cameraMoved = false;
        musicController?.PlayMusic();
        finishPanel.SetActive(false);
    }

    public void NextLevel()
    {
        Debug.Log("Next Button Clicked!");

    // पहले एड दिखाने का प्रयास करें
    AdManager adManager = FindObjectOfType<AdManager>();
    if (adManager != null)
    {
        adManager.ShowInterstitialAd();
    }
    else
    {
        Debug.LogError("AdManager not found in the scene.");
    }

    // फिर लेवल चेंज करें
    if (Level4 != null)
    {
        Level4.SetActive(false);
        Debug.Log("Level 4 Disabled");
    }

    if (Level5 != null)
    {
        Level5.SetActive(true);
        Debug.Log("Level 5 Enabled");
    }
    
    finishPanel.SetActive(false);
    musicController?.PlayMusic();
}
}


    



