using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float speed = 3f;  // Speed of movement
    public float pathWidth = 23f;  // Total movement width
    private Vector3 leftLimit, rightLimit;
    private int direction = 1;  // 1 = moving right, -1 = moving left

    void Start()
    {
        // Store the left and right boundary positions
        leftLimit = new Vector3(transform.position.x - (pathWidth / 2), transform.position.y, transform.position.z);
        rightLimit = new Vector3(transform.position.x + (pathWidth / 2), transform.position.y, transform.position.z);
    }

    void Update()
    {
        // Move the wall left and right
        transform.position += Vector3.right * speed * direction * Time.deltaTime;

        // Check if the wall reached the left or right limit
        if (transform.position.x >= rightLimit.x)
        {
            transform.position = rightLimit; // Correct any overshoot
            direction = -1; // Reverse direction
        }
        else if (transform.position.x <= leftLimit.x)
        {
            transform.position = leftLimit; // Correct any overshoot
            direction = 1; // Reverse direction
        }
    }

} 
