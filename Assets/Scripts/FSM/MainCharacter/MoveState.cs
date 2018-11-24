using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{

    private PlayerController MyPlayerController;
    private FSM MyFsm;

    public void OnEnterState()
    {
        //When we enter the state, we find the player's PlayerController and its fsm to send the condition has changed.
        if (MyPlayerController == null)
        {
            MyPlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        if (MyFsm == null)
        {
            MyFsm = GameObject.Find("Player").GetComponent<FSM>();
        }

        MyFsm.SetFSMCondition("is_moving", true);
    }

    public void UpdateState()
    {
        MyPlayerController.Move(InputManager.GetJoystickMovement());
    }

    public void OnExitState()
    {
        MyFsm.PopState();
    }
}
