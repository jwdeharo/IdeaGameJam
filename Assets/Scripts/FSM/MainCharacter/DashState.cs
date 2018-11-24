using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : IState {

    private const float DashSpeed = 10.0f;
    private const float StartDashTime = 0.1f;

    private float DashTime;

    private PlayerController MyPlayerController;
    private FSM MyFsm;
    private GameObject ThePlayer;

    public void OnEnterState()
    {
        Debug.Log("Dash state enter");
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

        MyFsm.SetFSMCondition("is_dashing", true);
        DashTime = StartDashTime;
    }

    public void UpdateState()
    {
    }

    public void OnExitState()
    {
        //In this case, we want to pop from the stack of states and go back to the last state we remember.
        MyFsm.PopState();
    }
}
