using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 5f;      
    public float maxHorizontalMove = 7.1f; 
    public Animator animator;            
    public Rigidbody rb;                 
    private float targetX;               
    private float screenWidth;
    private bool isDead = false;        
    private bool hasStarted = false;     
    public bool shouldMove = true;      

    void Start()
    {
        screenWidth = Screen.width;
        rb = GetComponent<Rigidbody>();
        targetX = transform.position.x;
        animator.SetBool("isRunning", false); 
    }

    void Update()
    {
        if (!isDead && shouldMove)  
        {
            if (Input.touchCount > 0) 
            {
                if (!hasStarted)
                {
                    hasStarted = true;
                    animator.SetBool("isRunning", true);
                }

                Touch touch = Input.GetTouch(0);
                DetectHorizontalMovement(touch);
            }
        }
    }

    void FixedUpdate()
    {
        if (hasStarted && !isDead && shouldMove)  
        {
            MoveForward();
            MoveHorizontally();
        }
    }

    void MoveForward()
    {
        shouldMove = true;
        rb.MovePosition(rb.position + Vector3.forward * forwardSpeed * Time.fixedDeltaTime);
    }

    void DetectHorizontalMovement(Touch touch)
    {
        float touchX = (touch.position.x - screenWidth / 3) / (screenWidth / 3);
        targetX = Mathf.Clamp(touchX * maxHorizontalMove, -maxHorizontalMove, maxHorizontalMove);
    }

    void MoveHorizontally()
    {
        float smoothX = Mathf.Lerp(transform.position.x, targetX, Time.fixedDeltaTime * 6);
        float pathWidth = 23f;
        float minX = -pathWidth / 18;
        float maxX = pathWidth / 3;

        smoothX = Mathf.Clamp(smoothX, minX, maxX);
        rb.MovePosition(new Vector3(smoothX, rb.position.y, rb.position.z));
    }

    public void StopMovingCompletely()  // Called when touching Blade
    {
        isDead = true;
        animator.SetTrigger("Die");
        rb.velocity = Vector3.zero;
        StartCoroutine(RestartLevel());
    }

    public void Die()  
    {
        if (!isDead)
        {
            isDead = true;
            animator.SetTrigger("Die");
            rb.velocity = Vector3.zero;  
            StartCoroutine(RestartLevel());
        }
    }
     public void StartRunning()
     {
        shouldMove = true;
        animator.SetBool("isRunning", true);
        rb.velocity = new Vector3(1f, rb.velocity.y, 0f);
     }

     public void StopRunningWhenTouchedRedEnemy()
     {
        animator.SetTrigger("Attack");
        shouldMove = false;
        rb.velocity = Vector3.zero;
     }

     public void StartRunningWhenDestryedEnemy()
     {
        MoveForward();
        animator.SetBool("isRunning", true);
     }


    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
