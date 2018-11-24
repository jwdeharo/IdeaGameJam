using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : IState {

    private PlayerController MyPlayerController;
    private FSM MyFsm;

    public void OnEnterState()
    {
        //When we enter the state, we find the player's PlayerController and its fsm to send the condition has changed.
        MyPlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        MyFsm = GameObject.Find("Player").GetComponent<FSM>();
    }

    public void UpdateState()
    {

    }

    public void OnExitState()
    {
    }
}
