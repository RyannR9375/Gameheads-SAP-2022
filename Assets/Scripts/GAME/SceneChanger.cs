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
        if (CompareTag("PreviousLevel") && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if(CompareTag("BossLevel") && other.CompareTag("Player") && GameManager.MyInstance.CollectedItems >= GameManager.MyInstance.victoryCondition)
        {
            SceneManager.LoadScene("BossLevel");
        }
        #endregion
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
        SceneManager.LoadScene("MainMenu");
    }
    #endregion

}
