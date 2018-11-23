using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//IState is an interface that will help us to implement
//a FSM for any object we want.
public interface IState  
{
    void OnEnterState();
    void OnExitState();
    void UpdateState();
}
