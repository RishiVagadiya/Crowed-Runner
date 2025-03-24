using System.Collections;
using UnityEngine;
using UnityEngine.Android; // For Vibration on Android

public class EnemyGroupController : MonoBehaviour
{
    public Animator[] enemyAnimators;
    public AudioSource battleSound;
    public Transform bluePlayersParent; // सभी ब्लू प्लेयर्स के Parent Object को सेट करें

    private Transform[] mergedEnemies;
    private Transform[] bluePlayers;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Followers"))
        {
            if (battleSound != null && !battleSound.isPlaying)
            {
                battleSound.Play(); // साउंड शुरू करो
            }

            GetAllBluePlayers(); // पहले ब्लू प्लेयर्स की लिस्ट ले लो
            DestroyEnemies();

            foreach (Animator anim in enemyAnimators)
            {
                anim.SetTrigger("Attack");
            }
        }
    }

    void GetAllBluePlayers()
    {
        if (bluePlayersParent != null)
        {
            bluePlayers = new Transform[bluePlayersParent.childCount];
            for (int i = 0; i < bluePlayersParent.childCount; i++)
            {
                bluePlayers[i] = bluePlayersParent.GetChild(i);
            }
        }
    }

    void DestroyEnemies()
    {
        mergedEnemies = new Transform[transform.childCount]; // सभी एनिमी को स्टोर करने के लिए Array
        int index = 0;

        foreach (Transform enemy in transform)
        {
            mergedEnemies[index] = enemy;
            index++;
        }

        StartCoroutine(DestroyEnemiesOneByOne());
    }

    IEnumerator DestroyEnemiesOneByOne()
    {
    if (mergedEnemies != null)
    {
        for (int i = 0; i < mergedEnemies.Length; i++)
        {
            if (mergedEnemies[i] != null)
            {
                mergedEnemies[i].gameObject.SetActive(false); // Enemy को Disable कर दो

                // अगर कोई ब्लू प्लेयर है, तो उसे भी Disable कर दो
                if (bluePlayers != null && i < bluePlayers.Length && bluePlayers[i] != null)
                {
                    bluePlayers[i].gameObject.SetActive(false);
                    FindObjectOfType<Correctanswer>().DisableBluePlayer(bluePlayers[i].gameObject);
                }

                VibratePhone(); // Vibrate कराओ
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    yield return new WaitForSeconds(0.001f); // आखिरी Enemy के बाद 1 सेकंड का वेट
    gameObject.SetActive(false); // पूरे Group को Disable कर दो

    FindObjectOfType<FollowerMovement>().StartRunningWhenDestryedEnemy();
    FindObjectOfType<PlayerMovement>().StartRunningWhenDestryedEnemy();

    if (battleSound != null)
    {
        battleSound.Stop();
    }
}

    void VibratePhone()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

            if (vibrator != null)
            {
                long[] pattern = { 0, 100 };
                vibrator.Call("vibrate", pattern, -1);
                Debug.Log("Vibration Triggered!");
            }
        #endif
    }
}
