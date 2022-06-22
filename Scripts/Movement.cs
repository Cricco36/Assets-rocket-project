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
    [SerializeField] float fuelRefilAmount = 50f;
    [SerializeField] float fuel = 100f;

    [Header("SFX")]
    [SerializeField] AudioClip EngineSFX;
    [SerializeField] AudioClip ExplosionSFX;
    [SerializeField] AudioClip LevelClearSFX;

    [Header("Aspect")]
    [SerializeField] ParticleSystem mainExhaustEffect;
    [SerializeField] ParticleSystem leftExhaustEffect;
    [SerializeField] ParticleSystem rightExhaustEffect;


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
            if(hasFuelLeft){
                ProcessThrust();
            }else{
                audioSource.Stop();
            }

            ProcessRotation();
        }        
        
    }

    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space)){
            myRigidbody.AddRelativeForce(Vector3.up * engineBoost * Time.deltaTime);  // Vector3.up == (0, 1, 0);
            burnFuel();
        }else{
            EnableExhaustEffect("main", false);
            audioSource.Stop();

        }
        
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.LeftArrow)){
            ApplyRotation(steeringAmount);
            
            EnableExhaustEffect("right", true);
        }else if(Input.GetKey(KeyCode.A)){
            ApplyRotation(steeringAmount);
            EnableExhaustEffect("right", true);
        }else{
            EnableExhaustEffect("right", false);
        }

        if (Input.GetKey(KeyCode.RightArrow)){
            ApplyRotation(-steeringAmount);
            EnableExhaustEffect("left", true);
        }else if (Input.GetKey(KeyCode.D)){
            ApplyRotation(-steeringAmount);
            EnableExhaustEffect("left", true);
        }else{
            EnableExhaustEffect("left", false);
        }
        
        
    }

    public void Die(){
        isDead = true;
        audioSource.Stop();
        audioSource.PlayOneShot(ExplosionSFX);
        EnableExhaustEffect("all", false);

        Invoke("Explode", explodeDelay);
        Invoke("ReloadLevel", reloadDelay);
    }

    public void Win(){
        hasWin = true;
        audioSource.Stop();
        audioSource.PlayOneShot(LevelClearSFX);
        EnableExhaustEffect("all", false);
    }

    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void Explode(){
        float x = Random.Range(-15f, 15f);
        float y = Random.Range(10f, 20f);
        myRigidbody.velocity = new Vector3( x, y, 0f);
        
    }

    public void addFuel(){
        fuel += fuelRefilAmount;
        hasFuelLeft = true;
    }

    void burnFuel(){
        if(!hasFuelLeft){
            audioSource.Stop();
            EnableExhaustEffect("all", false);
            return;
        }
        fuel -= 5f * Time.deltaTime;
        if(fuel <= 0){
            hasFuelLeft = false;
        }                                            //FFFFFFFFFFFFFUUUUUUUUUEEEEEEEEEEEEELLLLLLLLLLLLLLLLLLLLLLLL
        if(!audioSource.isPlaying){
            audioSource.PlayOneShot(EngineSFX);
        }
        if(!mainExhaustEffect.isPlaying){
            EnableExhaustEffect("main", true);
        }
    }
    
    void ApplyRotation(float steeringRotationAmount)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * steeringRotationAmount * Time.deltaTime); // Vector3.Forward == (0, 0, 1);
        myRigidbody.freezeRotation = false;

    }

    void EnableExhaustEffect(string boostPosition, bool status){
        switch (boostPosition)
        {
            case "right":
                if(status){
                    if(!rightExhaustEffect.isPlaying){
                        rightExhaustEffect.Play();
                    }
                }else{
                    rightExhaustEffect.Stop();
                }
                break;
            case "left":
                if(status){
                    if(!leftExhaustEffect.isPlaying){
                        leftExhaustEffect.Play();
                    }
                }else{
                    leftExhaustEffect.Stop();
                }
                break;
            case "main":
                if(status){
                    if(!mainExhaustEffect.isPlaying){
                        mainExhaustEffect.Play();
                    }
                }else{
                    mainExhaustEffect.Stop();
                }
                break;
            case "all":
                if(status){
                    if(!rightExhaustEffect.isPlaying){
                        rightExhaustEffect.Play();
                    }                    
                    if(!leftExhaustEffect.isPlaying){
                        leftExhaustEffect.Play();
                    } 
                    if(!mainExhaustEffect.isPlaying){
                        mainExhaustEffect.Play();
                    } 
                }else{
                    mainExhaustEffect.Stop();
                    leftExhaustEffect.Stop();
                    rightExhaustEffect.Stop();
                }
                break;

            default:
                Debug.Log("No exhaust called like this");
                break;
        }
    }

}
