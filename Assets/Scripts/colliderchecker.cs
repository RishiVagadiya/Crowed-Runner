using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderchecker : MonoBehaviour
{
     void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>(); // Scene ke sare objects lo

        foreach (GameObject obj in allObjects)
        {
            Collider[] colliders = obj.GetComponents<Collider>();
            if (colliders.Length > 1)
            {
                Debug.Log("Multiple Colliders Found on: " + obj.name);
            }
        }
    }
}
