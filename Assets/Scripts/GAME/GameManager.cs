using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Declaring Variables
    [SerializeField] private GameObject pauseUI;
    private static GameManager instance;
    [HideInInspector] public int CollectedItems, victoryCondition = 2;

    private bool GameOverYesOrNo;
    private bool isPaused = false;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    void Start()
    {
        UIManager.MyInstance.UpdateItemUI(CollectedItems, victoryCondition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }

        if (CollectedItems >= victoryCondition)
        {
            //SceneManager.LoadScene("BEAT GAME");
        }
    }
    

    public static GameManager MyInstance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();

            return instance;
        }
    }
    #endregion

    #region UI Changing Functions
    public void AddItems(int _items)
    {
        CollectedItems += _items;
        UIManager.MyInstance.UpdateItemUI(CollectedItems, victoryCondition);
    }

    public void SwitchPause()
    {
        if (GameOverYesOrNo == false)
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            isPaused = isPaused = true ? false : true;
        }
    }
    #endregion

    #region Loading Specific Scenes Functions
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    //MESSY
    public void Menu()
    {
        if (SceneManager.GetActiveScene().name == "GAME")
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("GAME");
    }

    public void Finish()
    {
        if(CollectedItems >= victoryCondition)
        {
            //CHANGE THIS TO BE ABLE TO ACCESS BOSS ROOM.
            SceneManager.LoadScene("BEAT GAME");
        }
    }
    #endregion
}
