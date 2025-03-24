using UnityEngine;

public class PlayerBlade : MonoBehaviour
{
    public ParticleSystem bloodEffect;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        bloodEffect.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blade") && CompareTag("Player"))
        {
            playerMovement.Die(); // Stop movement
            if (bloodEffect != null)
            {
                bloodEffect.Play();
            }
        }
    }
}
