using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField]Vector3 movementVector;
    [SerializeField] float period = 2f;

    float movementFactor;
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if(period  <= Mathf.Epsilon){return;}          //Mathf.Epsilon around 0
        float Cycles = Time.time / period;             //ALways growing

        const float tau = Mathf.PI * 2;                //Constant value of 6.283
        float rawSignWave = Mathf.Sin(Cycles * tau);    //going drom -1 to 1 to -1 to 1...

        movementFactor = (rawSignWave + 1) / 2f;        //recalculated to gro from 0 to 1 so easier

        Vector3 offset = movementVector * movementFactor;   
        transform.position = startingPosition + offset;
    }
}
