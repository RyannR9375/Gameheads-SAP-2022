using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResilienceOrbDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ResilientDisplayText = null;
    public void SetDisplayNumber(int CurrentNumOrbs, int MaxNumOrbs)
    {
        ResilientDisplayText.text = string.Format("{0} of {1}", CurrentNumOrbs, MaxNumOrbs);
    }
}
