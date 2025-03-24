using System.Collections.Generic;
using UnityEngine;

public class BluePlayers : MonoBehaviour
{
    public float forwardSpeed = 5f;  
    public float horizontalSpeed = 3f;  
    public float spacing = 1f;  // Followers के बीच का स्पेस  
    public GameObject followerPrefab;  
    public int startFollowers = 5;  

    private List<GameObject> followers = new List<GameObject>();  
    private Vector3 touchStartPos;  
    private bool isMoving = false;  

    void Start()
    {
        SpawnFollowers(startFollowers);
    }

    void Update()
    {
        MoveForward();
        HandleInput();
        MoveFollowers();
    }

    void MoveForward()
    {
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            isMoving = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            float moveAmount = (Input.mousePosition.x - touchStartPos.x) / Screen.width * horizontalSpeed;
            transform.position += Vector3.right * moveAmount;
            touchStartPos = Input.mousePosition;
        }
    }

    void SpawnFollowers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject follower = Instantiate(followerPrefab, transform.position - Vector3.forward * (i + 1) * spacing, Quaternion.identity);
            followers.Add(follower);
        }
    }

    void MoveFollowers()
    {
        for (int i = 0; i < followers.Count; i++)
        {
            Vector3 targetPos = transform.position - Vector3.forward * (i + 1) * spacing;
            followers[i].transform.position = Vector3.Lerp(followers[i].transform.position, targetPos, Time.deltaTime * forwardSpeed);
        }
    }
}
