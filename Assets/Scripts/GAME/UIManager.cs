﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Declaring Variables
    Collectable collectable;

    [SerializeField] TextMeshProUGUI txtItems, txtVictoryCondition;
    [SerializeField] GameObject victoryCondition;
    private static UIManager instance;
    private int neededItems = 2;
    #endregion

    #region Unity Callback Functions
    //ENSURES ONLY ONE INSTANCE OF THE UI MANAGER IS ACTIVE AT A TIME.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }

        //KEEPS THE UI THROUGHOUT SCENE TRANSFERS.
        //DontDestroyOnLoad(this);
    }

    //IF THERE IS NO INSTANCE OF THIS, THEN CREATE ONE.
    public static UIManager MyInstance 
    {
        get
        {
            if (instance == null)
                instance = new UIManager();

            return instance;
        }

    }
    #endregion

    #region UI Specific Functions

    public void UpdateItemUI(int _items, int _victoryCondition)
    {
        //collectable.itemValue += _items;
        txtItems.text = "Resilience Collected: " + _items + "/" + _victoryCondition;
    }

    public void ShowVictoryCondition(int _items, int _victoryCondition)
    {
        neededItems = _victoryCondition - _items;
        victoryCondition.SetActive(true);
        txtVictoryCondition.text = "YOU NEED " + neededItems + " MORE RESILIENCE IN ORDER TO ENTER THE BOSS ROOM.";
    }
    public void HideVictoryCondition()
    {
        victoryCondition.SetActive(false);
    }
    #endregion
}
