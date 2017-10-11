using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasionState : WolfieBaseState
{

    private Wolfie _wolfie;
    private SteeringCharacter _steeringCharacter;
    Vector3 fleeDirection;

    public EvasionState(Wolfie wofile, SteeringCharacter steeringCharacter)
    {
        _wolfie = wofile;
        _steeringCharacter = steeringCharacter;

        fleeDirection = steeringCharacter.transform.position - steeringCharacter.center.transform.position;
        steeringCharacter.transform.Rotate(Vector3.up, 180);
    }
    public void Navigate()
    {
        _steeringCharacter.transform.position = new Vector3(_steeringCharacter.transform.position.x + fleeDirection.x * Time.deltaTime*5 , 0, _steeringCharacter.transform.position.z + fleeDirection.z * Time.deltaTime*5);
    }
    public void Sonar()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Hit the button");
            _wolfie.SetWolfieState(new WanderingState(_wolfie, _steeringCharacter));
        }

    }
}
