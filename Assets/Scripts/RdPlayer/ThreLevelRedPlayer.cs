using UnityEngine;

public class ThreLevelRedPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ThreelevelScore.instance.ResetScoreOnNewLevel();
            Destroy(gameObject);
        }
    }
}
