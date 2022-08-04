using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    [SerializeField]private Keyprompts _testkey = null;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        { _testkey.SetVisible(true); }

        if (Input.GetKeyDown(KeyCode.S))
        { _testkey.SetVisible(false); }
    }
}
