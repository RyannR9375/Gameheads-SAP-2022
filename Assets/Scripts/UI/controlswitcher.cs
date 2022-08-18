using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType
{
    Keyboard,
    Xbox
}
public class controlswitcher : MonoBehaviour
{
    [SerializeField] private ControlDisplay[] controlDisplays = null;
    public void SetVisibleControls (ControlType controlType)
    {
     foreach(var c in controlDisplays)
        {
            c.SetDisplayedControl(controlType);
        }
    }


}
