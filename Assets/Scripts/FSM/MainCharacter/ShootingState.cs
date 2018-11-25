using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : IState {

    private const float TIME_TO_SHOOT = 0.25f;

    private float timeRemaining;

    private PlayerController MyPlayerController;
    private FSM MyFsm;
    private GameObject ThePlayer;

    private GameObject bullet;
    private string condition;

    public ShootingState(GameObject bullet, string condition)
    {
        this.bullet = bullet;
        this.condition = condition;
    }

    public void OnEnterState()
    {

        Debug.Log("hola");
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

        timeRemaining = TIME_TO_SHOOT;
        MyPlayerController.GetMyAnimator().SetBool("Is_Dashing", true);
    }

    public void UpdateState()
    {
        if (timeRemaining <= 0.0f)
        {
            MyFsm.SetFSMCondition(condition, false);
            Vector3 DirectionOfShooting = MyPlayerController.GetDirection().normalized;
            MyPlayerController.Shoot(bullet, DirectionOfShooting);
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
