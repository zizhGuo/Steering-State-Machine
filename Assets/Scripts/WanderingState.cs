using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WanderingState : WolfieBaseState
{
    // public GameObject objectPooler;      --------------
    private NewObjectPoolerScript temp;      //++++++++++++++++
    private NewObjectPoolerScript chicken;
    
    private Vector3 V1;
    private Vector3 V2;
    public Vector3 _acceleration;
    public Vector3 _velocity;
    private float rotateTime;
    private float checkTime;
    public System.Random r;
    public System.Random r2;
    // public System.Random r2;

    private float force;
    private float rotateAngle;
    private float rotateAngle2;
    private int rotateDirection;

    private Wolfie _wolfie; // _wolfie's function is using SetWolfieState to change state
    private SteeringCharacter _steeringCharacter; // _steeringCharacter's function is steering wolfie and percept environment

    public WanderingState(Wolfie wolfie, SteeringCharacter steeringCharacter)
    {
        _wolfie = wolfie;
        _steeringCharacter = steeringCharacter;
        r = new System.Random((int)DateTime.Now.Ticks % int.MaxValue);
        r2 = new System.Random((int)DateTime.Now.Ticks % int.MaxValue);
        rotateDirection = r2.Next(0, 99);
        V1 = _steeringCharacter.transform.position - _steeringCharacter.center.transform.position;
        rotateAngle = r.Next(120, 240);
        checkTime = Time.time;
        rotateTime = Time.time;
        _velocity = new Vector3(0, 0f, 0);

        temp = _steeringCharacter.obstaclePooler.GetComponent<NewObjectPoolerScript>(); // +++++++++++++++++++
        chicken = _steeringCharacter.chickenPooler.GetComponent<NewObjectPoolerScript>();
    }


    public void Navigate() // Global Interfacial Method
    {
        _wolfie.globalVelocity = _velocity;
        ///<summary>
        ///Setup Timer for changing wandering direction
        /// </summary>
        if (Time.time - checkTime > 3)
        {
            rotateAngle = r.Next(120, 240);
            rotateDirection = r2.Next(0, 99);
            //rotateDirection = r2.Next(0, 1); // left or right

            checkTime = Time.time;
        }
        ///<summary>
        ///Setup Timer for turning around
        /// </summary>
        //if (Time.time - rotateTime > 10)
        //{
        //    _steeringCharacter.transform.Rotate(Vector3.up, 180);
        //    rotateTime = Time.time;
        //}
        if (rotateDirection < 50)
        {
            rotateAngle = rotateAngle + 0.1f;
        }
        else
        {
            rotateAngle = rotateAngle - 0.1f;
        }
        if (rotateAngle > 360)
        {
            rotateAngle = 0;
        }

        ///<summary>
        ///Calculate the walking force & rotate angle
        /// </summary>
        force = Mathf.Sqrt((float)(2.5625 - 2.5 * Mathf.Cos(rotateAngle * Mathf.Deg2Rad)));
        _steeringCharacter.pointer.GetComponent<Transform>().localPosition = new Vector3(-Mathf.Sin(rotateAngle * Mathf.Deg2Rad), 0, 1.25f - Mathf.Cos(rotateAngle * Mathf.Deg2Rad));
        V1 = Vector3.Normalize(_steeringCharacter.pointer.transform.position - _steeringCharacter.transform.position);
        _acceleration = new Vector3(V1.x * force * force * 10, 0f, V1.z * force * force * 10);
       // if (Mathf.Abs(_velocity.x) <0.3f && Mathf.Abs(_velocity.z)<0.3f )
        //{
            _velocity = Vector3.ClampMagnitude(new Vector3(_velocity.x + _acceleration.x * Time.deltaTime, 0f, _velocity.z + _acceleration.z * Time.deltaTime), 0.8f);
        //} 
        _steeringCharacter.transform.position = new Vector3(_steeringCharacter.transform.position.x + _velocity.x * Time.deltaTime, 0, _steeringCharacter.transform.position.z +_velocity.z * Time.deltaTime);

        if (rotateAngle < 180)
        {
            _steeringCharacter.transform.Rotate(Vector3.up, -Mathf.Acos((force * force + 0.5625f) / (2.5f * (float)force)));
        }
        else
        {
            _steeringCharacter.transform.Rotate(Vector3.up, Mathf.Acos((force * force + 0.5625f) / (2.5f * (float)force)));
        }
        ///<summary>
        ///Press "A" to place new Obstacle on map
        /// </summary>
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    PlaceObstacle();
        //}
        if (_steeringCharacter.transform.position.x > 5.8f)
        {
            _steeringCharacter.transform.position = new Vector3(-5.8f, 0, _steeringCharacter.transform.position.z);
        }
        if (_steeringCharacter.transform.position.x < -5.8f)
        {
            _steeringCharacter.transform.position = new Vector3(5.8f, 0, _steeringCharacter.transform.position.z);
        }
        if (_steeringCharacter.transform.position.z > 5.8f)
        {
            _steeringCharacter.transform.position = new Vector3(_steeringCharacter.transform.position.z, 0, -5.8f);
        }
        if (_steeringCharacter.transform.position.z < -5.8f)
        {
            _steeringCharacter.transform.position = new Vector3(_steeringCharacter.transform.position.z, 0, 5.8f);
        }

    }
    /// <summary>
    /// O(n) proformance real-time detection
    /// </summary>
    public void Sonar() // Global Interfacial Method  
    {
        ///<example>
        ///
        /// Testing (building connection between main character and other game objects)
        /// 
        /// </example>

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Hit the button");
            _wolfie.SetWolfieState(new EvasionState(_wolfie, _steeringCharacter));
        }
        for (int i = 0; i < temp.pooledAmount; i++)
        {
            Debug.DrawLine(temp.pooledObjects[i].transform.position, _steeringCharacter.transform.position, Color.red);
            //Detect the distance between obstacles and main characters
            if (temp.pooledObjects[i].activeInHierarchy && Vector3.Distance(temp.pooledObjects[i].transform.position, _steeringCharacter.transform.position)< 1)
            {
                
                //_wolfie.SetWolfieState(new EvasionState(_wolfie, _steeringCharacter));
            }
        }
        for (int i = 0; i < chicken.pooledAmount; i++)
        {
            //Debug.DrawLine(chicken.pooledObjects[i].transform.position, _steeringCharacter.transform.position, Color.red);
            if (chicken.pooledObjects[i].activeInHierarchy && Vector3.Distance(chicken.pooledObjects[i].transform.position, _steeringCharacter.transform.position) < 5)
            {
                _wolfie.SetWolfieState(new SeekingState(_wolfie, _steeringCharacter));
               // _steeringCharacter.transform.position = new Vector3(_steeringCharacter.transform.position.x + wanderInheritence._velocity.x * Time.deltaTime, 0, _steeringCharacter.transform.position.z + wanderInheritence._velocity.z * Time.deltaTime);
                break;
            }
            if (chicken.pooledObjects[i] == null) return;
        }

    }


    //void PlaceObstacle() // Local State Method   
    //{
        //for (int i = 0; i < temp.pooledAmount; i++)
        //{
        //    if (!temp.pooledObjects[i].activeInHierarchy)
        //    {
        //        temp.pooledObjects[i].transform.position = _steeringCharacter.transform.position;
        //        temp.pooledObjects[i].transform.rotation = _steeringCharacter.transform.rotation;
        //        temp.pooledObjects[i].SetActive(true);
        //        break;
        //    }
        //    if (temp.pooledObjects[i] == null) return;
        //}

        //pooledObstacles.Add(obj);
    //}
}
