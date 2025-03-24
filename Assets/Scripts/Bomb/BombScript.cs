using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameObject explosionEffect;  
    public AudioClip explosionSound;    
    private AudioSource audioSource;    
    public float explosionDelay = 0.5f; 

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);

            other.GetComponent<PlayerMovement>().Die();

            Destroy(gameObject, explosionSound.length);
        }
    }
}
