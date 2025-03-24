using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectLvl2 : MonoBehaviour
{
    public GameObject level1;  // Assign Level 2 GameObject in Inspector
    public GameObject level2;
    public GameObject level3;  // Assign Level 3 GameObject in Inspector

    void Start()
    {
        // Ensure only the current level is active at the start
        level1.SetActive(true);
        level2.SetActive(false);
        level3.SetActive(false);
    }

    public void NextLevel()
    {
        Debug.Log("Next Level Button Clicked!");
        // Disable Level 2 and Enable Level 3
        level2.SetActive(false);
        level3.SetActive(true);
        
        Debug.Log("Level 2 Disabled, Level 3 Enabled!");
    }
}
