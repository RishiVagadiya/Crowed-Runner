using UnityEngine;

public class FourRP : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FourLevelScore.instance.ResetScoreOnNewLevel();
            Destroy(gameObject);
        }
    }
}
