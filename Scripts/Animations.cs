using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{

    Animator myAnimator;
    
    Animation landingAnim;
    Rigidbody myRigidbody;

    private void Start() {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody>();

    }



    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Friendly"){
            myAnimator.SetFloat("attivaGambeVel", 1);
            myAnimator.SetBool("isFlying", true);
            myAnimator.SetBool("isLanding", false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Friendly" || other.gameObject.tag == "Finish"){
            float multipler = -myRigidbody.velocity.y * 0.5f;
            myAnimator.SetFloat("attivaGambeVel", multipler);
            myAnimator.SetBool("isLanding", true);
            myAnimator.SetBool("isFlying", false);
        }
    }


}
