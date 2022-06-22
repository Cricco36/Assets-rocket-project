using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BumpListener : MonoBehaviour
{

    float nextLevelDelay = 5f;

    Movement movementScript;
    AudioSource audioSource;

    [SerializeField] AudioClip LevelClearSFX;
    private void Start() {
        movementScript = GetComponent<Movement>();        
        audioSource = GetComponent<AudioSource>();

    }

    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag)
        {
            case "Friendly":

                break;
            case "Finish":
                
                Landed();
                break;
            default:
                movementScript.Die();
                break;
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Fuel"){
            Destroy(other.gameObject);
            movementScript.addFuel();
        }
        
    }

    void Landed(){
        /*checkForOkLanding();     CORUTINE nextLevelDelay

            if(noInput(3 secondi(più di quanti ce ne metta a resettare la die method)) == true && collidertrigger pk non
            gestite collisioni con ground friendly){

            }
            
        */
        //only after
        movementScript.Win();
        float originalPitch = audioSource.pitch;
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(LevelClearSFX);
        Invoke("NextLevel", LevelClearSFX.length + 1f); 
    }

    void NextLevel(){
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
