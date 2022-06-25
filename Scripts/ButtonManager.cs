using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{   
    public void Play(){
        SceneManager.LoadScene("Basics");
    }

    public void OpenSettings(){
        SceneManager.LoadScene("A_Settings");
    }

    public void Quit(){
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("A_Settings")){
            SceneManager.LoadScene("A_Menu");
        }
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("A_Menu")){
            Application.Quit();
        }
    }
}
