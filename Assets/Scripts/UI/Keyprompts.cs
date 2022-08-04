using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyprompts : MonoBehaviour
{
  public void SetVisible(bool visible)
    { 
        if (visible)
        {
            GetComponent<Animator>().SetTrigger("ShowTrigger");


        }

        else
        {
            GetComponent<Animator>().SetTrigger("HideTrigger");
        }        
    }
        
}
