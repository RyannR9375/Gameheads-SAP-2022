using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// DontDestroyOnLoad Player, Health, UI, Collectable Values
    /// Potentially creating save files,
    /// saving scenes once you leave them
    /// </summary>

    #region Collision Functions
    private void OnTriggerEnter2D(Collider2D other)
    {
        #region Next Level on Build
        if (CompareTag("NextLevel") && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        #endregion

        #region Previous Level on Build
        else if (CompareTag("PreviousLevel") && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        else if(CompareTag("BossRoomDoor") && other.CompareTag("Player") && GameManager.MyInstance.CollectedItems >= GameManager.MyInstance.victoryCondition)
        {
            SceneManager.LoadScene("BossLevel");
        }
        #endregion

        if(other.CompareTag("Player") && this.CompareTag("ChangerMain"))
        {
            SceneManager.LoadScene("MainLevel");
        }

        if (other.CompareTag("Player") && this.CompareTag("ChangerBoss"))
        {
            SceneManager.LoadScene("BossLevel");
        }
    }
    #endregion

    #region Functions for Buttons
    //JUST GETS NEXT SCENE IN THE BUILD
    public void NextScene()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    //QUITS APPLICATION
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    // ..?
    public void MainMenu()
    {
        SceneManager.LoadScene("titlescene");
    }

    public void RestartScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoadEmpty()
    {
        SceneManager.LoadScene("Empty");
    }

    public void LoadEmptyBoss()
    {
        SceneManager.LoadScene("Empty1");
    }
    #endregion

}
