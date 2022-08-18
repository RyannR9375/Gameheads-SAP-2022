using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDisplay : MonoBehaviour
{
    [SerializeField]private GameObject KeyboardControl= null;
    [SerializeField]private GameObject XboxControl = null;

    public void SetDisplayedControl (ControlType controlType)
    {
        KeyboardControl.SetActive(controlType == ControlType.Keyboard);
        XboxControl.SetActive(controlType == ControlType.Xbox);
    }
}
