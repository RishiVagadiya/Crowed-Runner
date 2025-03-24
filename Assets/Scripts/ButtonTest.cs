using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(() => Debug.Log("Button Clicked: " + gameObject.name));
        else
            Debug.LogError("Button component is missing on " + gameObject.name);
    }
}
