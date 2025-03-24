using UnityEngine;

public class RPLevel2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Leveltwo.instance.ResetScoreOnNewLevel();
            Destroy(gameObject);
        }
    }
}
