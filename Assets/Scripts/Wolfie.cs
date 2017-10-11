using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolfie : WolfieBaseState
{

   // private SteeringCharacter steeringCharacter;
    public WolfieBaseState _state;
    private SteeringCharacter _steeringCharacter;
    public Vector3 globalVelocity;

    public Wolfie(SteeringCharacter steeringCharacter)
    {
        _steeringCharacter = steeringCharacter;
         _state = new WanderingState(this, _steeringCharacter);
    }
    public void SetWolfieState(WolfieBaseState newState)
    {
        _state = newState;
    }

    // Update is called once per frame
    public void Navigate()
    {
        _state.Navigate();
    }
    public void Sonar()
    {
        _state.Sonar();
    }
}
