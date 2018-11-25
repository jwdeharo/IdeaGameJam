using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashState : EnemyBaseState, IState
{

    private const float DashSpeed = 5.0f;
    private const float StartDashTime = 0.1f;

    private float DashTime;

    private EnemyController MyController;
    private GameObject      ThePlayer;
    private FSM MyFsm;

    public void OnEnterState()
    {
        //When we enter the state, we find the player's PlayerController and its fsm to send the condition has changed.

        if (ThePlayer == null)
        {
            ThePlayer = GameObject.Find("Player");
        }

        if (MyController == null)
        {
            MyController = MyGameObject.GetComponent<EnemyController>();
        }

        if (MyFsm == null)
        {
            MyFsm = MyGameObject.GetComponent<FSM>();
        }

        DashTime = StartDashTime;
        //MyPlayerController.GetMyAnimator().SetBool("Is_Dashing", true);
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
            Vector3 PlayerPosition = ThePlayer.transform.position;
            Vector3 DashMovement = PlayerPosition - MyGameObject.transform.position;
            MyController.Move(DashMovement * DashSpeed, true);
        }
    }

    public void OnExitState()
    {
        //In this case, we want to pop from the stack of states and go back to the last state we remember.
        MyFsm.PopState();
    }
}
