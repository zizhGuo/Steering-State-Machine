using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SteeringCharacter : MonoBehaviour
{
    public GameObject center;
    public GameObject pointer;
    public GameObject obstaclePooler;
    public GameObject chickenPooler;
    public GameObject wolfiePooler;
    //public GameObject mainCharater;
    

    private Wolfie wolfie;
    void Start()
    {
        //temp = obstaclePooler.GetComponent<NewObjectPoolerScript>();

        wolfie = new Wolfie(this);

        Debug.Log("Rotate degree 30: " + Mathf.Cos(30 * Mathf.Deg2Rad));

    }

    void FixedUpdate()
    {
        wolfie.Navigate();
        wolfie.Sonar();
    }

}
