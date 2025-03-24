using UnityEngine;

public class CollisionDebugger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter: " + other.gameObject.name + " | Tag: " + other.gameObject.tag);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter: " + collision.gameObject.name + " | Tag: " + collision.gameObject.tag);
    }
}
