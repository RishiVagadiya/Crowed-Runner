using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowesFinishLine : MonoBehaviour
{
   public Animator animator;

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Followers"))
        {
            animator.SetTrigger("Victory");
        }
    }
}
