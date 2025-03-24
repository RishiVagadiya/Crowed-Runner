using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class FinishLineLeve1 : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform playerStartPosition;
    private GameObject player;
    public GameObject finishPanel;
    public GameObject Level1;
    public GameObject Level2;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI levelCompleteText;
    public Button retryButton;
    public Button nextLevelButton;

    [Header("Game Settings")]
    public GameObject[] levels;
    public int currentLevel = 0;

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
    private const int RequiredScore = 30;
    private Animator playerAnimator;
    private bool cameraMoved = false;

    private void Start()
    { 
        ScoreManager.instance.ResetScoreOnNewLevel();

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
             //Debug.Log("Mouse Clicked at: " + Input.mousePosition);
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
            levelCompleteText.text = "DEFEAT! Score at least 30!";
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
        return ScoreManager.instance != null ? ScoreManager.instance.currentScore : 0;
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
        Debug.Log("retry Button Clicked!");

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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        musicController?.PlayMusic();
       

        cameraMoved = false;
        musicController?.PlayMusic();
        finishPanel.SetActive(false);
    }

    public void NextLevel()
{
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
    if (Level1 != null)
    {
        Level1.SetActive(false);
        Debug.Log("Level 1 Disabled");
    }

    if (Level2 != null)
    {
        Level2.SetActive(true);
        Debug.Log("Level 2 Enabled");
    }
    
    finishPanel.SetActive(false);
    musicController?.PlayMusic();
}

}


