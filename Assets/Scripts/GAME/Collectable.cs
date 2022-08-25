﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collected();
        }
    }

    protected virtual void Collected()
    {
        //override
        Destroy(this.gameObject);
    }
}
