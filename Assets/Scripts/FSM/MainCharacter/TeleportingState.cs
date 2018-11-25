using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingState : IState {

    private const float TIME_TO_TELEPORT = 1f;

    private float timeRemaining;

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

        timeRemaining = TIME_TO_TELEPORT;
        MyPlayerController.GetMyAnimator().SetBool("Is_Dashing", true);
    }

    public void UpdateState()
    {
        if (timeRemaining <= 0.0f)
        {
            MyFsm.SetFSMCondition("is_tping", false);
            Vector3 TpMovement = MyPlayerController.GetDirection() * 300;
            MyPlayerController.Move(TpMovement, true);
        }
        else
        {
            timeRemaining -= Time.deltaTime;
            
        }
    }

    public void OnExitState()
    {
        //In this case, we want to pop from the stack of states and go back to the last state we remember.
        MyFsm.PopState();
        MyPlayerController.GetMyAnimator().SetBool("Is_Dashing", false);
    }
}
