using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{

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
    }

    public void UpdateState()
    {
        Vector3 LastPosition = ThePlayer.transform.position;
        Vector3 Movement = InputManager.GetJoystickMovement();

        if (Movement != Vector3.zero)
        {
            MyPlayerController.Move(Movement);
            MyPlayerController.SetDirection((ThePlayer.transform.position - LastPosition).normalized);
        }
    }

    public void OnExitState()
    {
       MyFsm.PopState();
    }
}
