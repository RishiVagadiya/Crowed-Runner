using UnityEngine;

public class FollowerMovement : MonoBehaviour
{
    public Animator[] followersAnimators; 
    private bool shouldMove = true; 

    void Start()
    {
        followersAnimators = GetComponentsInChildren<Animator>();
    }

    void Update()
    {
        if (!shouldMove) return; 

        if (Input.GetMouseButtonDown(0)) 
        {
            foreach (Animator anim in followersAnimators)
            {
                anim.SetBool("Run", true);
            }
            //Debug.Log("All Followers Started Running!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            shouldMove = false; 
            FindObjectOfType<PlayerMovement>().StopRunningWhenTouchedRedEnemy();
            foreach (Animator anim in followersAnimators)
            {
                anim.SetBool("Run", false);
                anim.SetBool("Attack", true);
                //Debug.Log("runing aniamtion stopeed");
            }

            //Debug.Log("All Followers Stopped!");
        }
    }

    private void RemoveDestroyedFollowers()
    {
        followersAnimators = System.Array.FindAll(followersAnimators, anim => anim != null);
    }

    public void StartRunningWhenDestryedEnemy()
    {
         shouldMove = true;
    
         RemoveDestroyedFollowers(); // 👈 Destroyed Animators को Remove करो

       foreach (Animator anim in followersAnimators)
       {
           if (anim != null) 
           {
               anim.SetBool("Run", true);
               anim.SetBool("Attack", false);
           }
       }
    }
}
