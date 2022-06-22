using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevKeys : MonoBehaviour
{
    [SerializeField] bool devKeys = true;    

    //MeshCollider m_MeshCollider;

    private Scene currentScene;
    void Start()
    {
        //m_MeshCollider = GetComponent<MeshCollider>();
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (devKeys)
        {
            LoadNextLevel();
            ReloadLevel();
            ToggleCollisions();
        }      
    }

    void ToggleCollisions()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (this.GetComponent<BumpListener>().canCollide)
            {
                this.GetComponent<BumpListener>().canCollide = false;
            }
            else if(!this.GetComponent<BumpListener>().canCollide)
            {
                this.GetComponent<BumpListener>().canCollide = true;
            }
        }       
    }
    void ReloadLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
    void LoadNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (currentScene.buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(currentScene.buildIndex + 1);
            }
            else if (currentScene.buildIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
