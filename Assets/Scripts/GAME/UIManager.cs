using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Declaring Variables
    [SerializeField] TextMeshProUGUI txtItems, txtVictoryCondition;
    [SerializeField] GameObject victoryCondition;
    private static UIManager instance;
    private int neededItems;
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
        DontDestroyOnLoad(this);
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
        txtItems.text = "Items Collected: " + _items + "/" + _victoryCondition;
    }

    public void ShowVictoryCondition(int _items, int _victoryCondition)
    {
        neededItems = _victoryCondition - _items;
        victoryCondition.SetActive(true);
        txtVictoryCondition.text = "YOU NEED " + neededItems + " IN ORDER TO ENTER THE BOSS ROOM.";
    }
    public void HideVictoryCondition()
    {
        victoryCondition.SetActive(false);
    }
    #endregion
}
