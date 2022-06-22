using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float engineBoost = 800f;
    [SerializeField] float steeringAmount = 150f;

    [Header("Gameplay")]
    [SerializeField] float FuelRefilAmount = 50f;
    [SerializeField] float fuel = 100f;

    [Header("SFX")]
    [SerializeField] AudioClip EngineSFX;
    [SerializeField] AudioClip ExplosionSFX;


    //PLAYER VARIABILES
    bool hasFuelLeft = true;
    bool isDead = false;
    bool hasWin = false;
    float reloadDelay = 3f;
    float explodeDelay = 0.5f;


    //COMPONENTS
    Rigidbody myRigidbody;
    AudioSource audioSource;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if(!isDead && !hasWin){
            if(hasFuelLeft)
            {
                ProcessThrust();   
            }else{
                audioSource.Stop();
            }

            ProcessRotation();
        }        
        
    }

    public void Die(){
        isDead = true;
        Invoke("Explode", explodeDelay);
        Invoke("ReloadLevel", reloadDelay);
    } 

    public void Win(){
        hasWin = true;
    }

    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void Explode(){
        float x = Random.Range(-15f, 15f);
        float y = Random.Range(10f, 20f);
        myRigidbody.velocity = new Vector3( x, y, 0f);
        
        audioSource.PlayOneShot(ExplosionSFX);
    }

    public void addFuel(){
        fuel += FuelRefilAmount;
        hasFuelLeft = true;
    }

    void burnFuel(){
        if(!hasFuelLeft){
            audioSource.Stop();
            return;
        }
        fuel -= 5f * Time.deltaTime;
        if(fuel <= 0){
            hasFuelLeft = false;
        }                                            //FFFFFFFFFFFFFUUUUUUUUUEEEEEEEEEEEEELLLLLLLLLLLLLLLLLLLLLLLL
        if(!audioSource.isPlaying){
            audioSource.PlayOneShot(EngineSFX);
        }
    }

    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space)){
            myRigidbody.AddRelativeForce(Vector3.up * engineBoost * Time.deltaTime);  // Vector3.up == (0, 1, 0);
            burnFuel();
        }else{
            audioSource.Stop();
        }
        
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.LeftArrow)){
            ApplyRotation(steeringAmount);
        }else if(Input.GetKey(KeyCode.A)){
            ApplyRotation(steeringAmount);
        }
        
        if (Input.GetKey(KeyCode.RightArrow)){
            ApplyRotation(-steeringAmount);
        }else if (Input.GetKey(KeyCode.D)){
            ApplyRotation(-steeringAmount);
        }
        
        
    }

    private void ApplyRotation(float steeringRotationAmount)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * steeringRotationAmount * Time.deltaTime); // Vector3.Forward == (0, 0, 1);
        myRigidbody.freezeRotation = false;

    }


}
