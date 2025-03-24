using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryLeveltwo : MonoBehaviour
{
    public GameObject level2;
    private List<GameObject> disabledObjects = new List<GameObject>();

    void Start()
    {
        // जो भी Objects Start में Disabled हैं, उन्हें Store कर लो
        foreach (Transform obj in level2.transform)
        {
            if (!obj.gameObject.activeSelf)
            {
                disabledObjects.Add(obj.gameObject);
            }
        }
    }

    public void RetryLevel2()
    {
        level2.SetActive(false);
        Invoke("EnableLevel2", 0.1f);
    }

    void EnableLevel2()
    {
        level2.SetActive(true);

        // सभी Objects को Enable करो
        foreach (Transform obj in level2.transform)
        {
            obj.gameObject.SetActive(true);
        }

        // पहले से Disabled Objects को वापस Disable कर दो
        foreach (GameObject obj in disabledObjects)
        {
            obj.SetActive(false);
        }
    }
}
