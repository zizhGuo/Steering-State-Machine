using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingState : WolfieBaseState
{
    public GameObject chickenPooler;

    private NewObjectPoolerScript chicken;

    private Wolfie _wolfie;
    private SteeringCharacter _steeringCharacter;
    Vector3 seekDirection;
    bool isHunting;
    int targetNum;

    public SeekingState(Wolfie wofile, SteeringCharacter steeringCharacter)
    {
        _wolfie = wofile;
        _steeringCharacter = steeringCharacter;
        chicken = _steeringCharacter.chickenPooler.GetComponent<NewObjectPoolerScript>();
        isHunting = false;       
    }

    public void Navigate()
    {
        if (isHunting)
        {
            Debug.DrawLine(chicken.pooledObjects[targetNum].transform.position, _steeringCharacter.transform.position, Color.red);
            _steeringCharacter.transform.position = new Vector3(_steeringCharacter.transform.position.x + Vector3.Normalize(seekDirection).x * Time.deltaTime* 2, 0, _steeringCharacter.transform.position.z + Vector3.Normalize(seekDirection).z * Time.deltaTime* 2);
            if (Vector3.Distance(chicken.pooledObjects[targetNum].transform.position, _steeringCharacter.transform.position) < 0.2)
            {
                chicken.pooledObjects[targetNum].SetActive(false);
                _wolfie.SetWolfieState(new WanderingState(_wolfie, _steeringCharacter));
            }
        }
    }
    public void Sonar()
    {
        for (int i = 0; i < chicken.pooledAmount; i++)
        {
            
            if (chicken.pooledObjects[i].activeInHierarchy && Vector3.Distance(chicken.pooledObjects[i].transform.position, _steeringCharacter.transform.position) < 5)
            {
                isHunting = true;
                targetNum = i;
                seekDirection = chicken.pooledObjects[i].transform.position - _steeringCharacter.transform.position;
                _steeringCharacter.transform.LookAt(chicken.pooledObjects[i].transform.position);
                break;
            }
            if (chicken.pooledObjects[i] == null) return;
            if (isHunting)
            {
                break;
            }
        }

    }
}
