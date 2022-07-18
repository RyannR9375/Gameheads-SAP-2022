using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUITesting : MonoBehaviour
{

    public Button buttonTest;

    void Start()
    {
        Button btn = buttonTest.GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        Debug.Log("button");
    }
}
