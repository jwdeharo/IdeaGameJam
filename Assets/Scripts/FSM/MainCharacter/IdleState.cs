using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private GameObject          ThePlayer;
    private PlayerController    MyPlayerController;
    private FSM                 MyFsm;

    public void OnEnterState()
    {
        //When we enter the state, we find the player's PlayerController and its fsm to send the condition has changed.
        if (ThePlayer == null)
        {
            ThePlayer = GameObject.Find("Player");
        }

        if (MyPlayerController == null)
        {
            MyPlayerController = ThePlayer.GetComponent<PlayerController>();
        }

        if (MyFsm == null)
        {
            MyFsm = ThePlayer.GetComponent<FSM>();
        }

        MyFsm.SetFSMCondition("is_moving", false);
        MyFsm.SetFSMCondition("is_dashing", false);
    }

    public void UpdateState()
    {
        MyPlayerController.SetLastPosition(ThePlayer.transform.position);
    }

    public void OnExitState()
    {
    }
}
