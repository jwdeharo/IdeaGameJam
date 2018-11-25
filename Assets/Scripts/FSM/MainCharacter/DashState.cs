using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : IState {

    private const float DashSpeed = 10.0f;
    private const float StartDashTime = 0.4f;

    private float DashTime;

    private PlayerController MyPlayerController;
    private FSM MyFsm;
    private GameObject ThePlayer;
    
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
        DashTime = StartDashTime;
        MyPlayerController.GetMyAnimator().SetBool("Is_Dashing", true);
    }

    public void UpdateState()
    {
        if (DashTime <= 0.0f)
        {
            MyFsm.SetFSMCondition("is_dashing", false);
        }
        else
        {
            DashTime -= Time.deltaTime;
            Vector3 DashMovement = MyPlayerController.GetDirection() * DashSpeed;
            MyPlayerController.Move(DashMovement, true);
        }
    }

    public void OnExitState()
    {
        //In this case, we want to pop from the stack of states and go back to the last state we remember.
        MyFsm.PopState();
        MyPlayerController.GetMyAnimator().SetBool("Is_Dashing", false);
    }
}
