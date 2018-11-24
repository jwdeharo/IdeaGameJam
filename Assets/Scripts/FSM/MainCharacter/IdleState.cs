﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private PlayerController    MyPlayerController;
    private FSM                 MyFsm;

    public void OnEnterState()
    {
        //When we enter the state, we find the player's PlayerController and its fsm to send the condition has changed.
        MyPlayerController  =   GameObject.Find("Player").GetComponent<PlayerController>();
        MyFsm               =   GameObject.Find("Player").GetComponent<FSM>();
    }

    public void UpdateState()
    {
        //The UpdateState does not do much yet. 
        if (InputManager.GetJoystickMovement() != Vector3.zero)
        {
            //If the input is different from 0, then this means that we're moving.
            MyFsm.SetFSMCondition("is_moving", true);
        }
        else
        {
            MyFsm.SetFSMCondition("is_moving", false);
        }
    }

    public void OnExitState()
    {
    }
}
