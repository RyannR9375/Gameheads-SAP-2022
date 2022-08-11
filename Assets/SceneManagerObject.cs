using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerObject : MonoBehaviour
{
    [SerializeField]
    //public Object NextSceneObject;
   
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NextScene(Object scene)
    {
        if (scene.ToString().Contains("(UnityEngine.SceneAsset)"))
        {
            SceneManager.LoadScene(scene.name);
        }

    }
}
