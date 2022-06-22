using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BumpListener : MonoBehaviour
{

    [SerializeField] ParticleSystem finishParticle;
    [SerializeField] ParticleSystem crashParticle;


    float nextLevelDelay = 5f;
    bool isTransitioning = false;
    bool isDying = false;
    public bool canCollide;

    Movement movementScript;

    private void Start() {
        movementScript = GetComponent<Movement>();  
        canCollide = true;
    }

    void OnCollisionEnter(Collision other) {
        if(isTransitioning || isDying || !canCollide){return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("ur on a checkpoint, congrats!");
                break;
            case "Finish":
                Finish();
                break;
            default:
                Crash();
                
                break;
        }
        
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Fuel"){
            Destroy(other.gameObject);
            movementScript.addFuel();
        }
        
    }

    void Crash(){
        isDying = true;
        movementScript.Die();
        crashParticle.Play();
    }

    void Finish(){
        Landed();
        finishParticle.Play();
    }



    void Landed(){
        /*checkForOkLanding();     CORUTINE nextLevelDelay

            if(noInput(3 secondi(più di quanti ce ne metta a resettare la die method)) == true && collidertrigger pk non
            gestite collisioni con ground friendly){

            }
            
        */
        //only after
        movementScript.Win();
        isTransitioning = true;
        Invoke("NextLevel", 3f); 
    }

    public void NextLevel(){
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
