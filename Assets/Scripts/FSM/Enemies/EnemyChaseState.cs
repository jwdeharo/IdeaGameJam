using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState, IState
{
    private FSM MyFsm;
    private CharacterController MyController;
    private GameObject ThePlayer;
    private EnemyController MyEnemyController;
    private Animator MyAnimator;
    private bool IsShocking;

    public void OnEnterState()
    {
        if (MyFsm == null)
        {
            MyFsm = MyGameObject.GetComponent<FSM>();
        }

        if (MyController == null)
        {
            MyController = MyGameObject.GetComponent<CharacterController>();
        }

        if (ThePlayer == null)
        {
            ThePlayer = GameObject.Find("Player");
        }

        if (MyEnemyController == null)
        {
            MyEnemyController = MyGameObject.GetComponent<EnemyController>();
        }

        if (MyAnimator == null)
        {
            MyAnimator = MyGameObject.GetComponent<Animator>();
        }

        IsShocking = false;

        MyAnimator.SetBool("Is_chasing", true);
    }

    public void OnExitState()
    {
        if (!IsShocking)
        {
            MyAnimator.SetBool("Is_chasing", false);
        }

        MyFsm.PopState();
    }

    public void UpdateState()
    {
        //We calculate the distance to the target.
        Vector3 DistanceToWaypoint = ThePlayer.transform.position - MyGameObject.transform.position;
        MyEnemyController.Move(DistanceToWaypoint);

        if (DistanceToWaypoint.magnitude > 10.0f)
        {
            MyFsm.SetFSMCondition("start_chasing", false);
        }
        else if (DistanceToWaypoint.magnitude < 1.0f)
        {
            int RandomValue = Random.Range(0, 1000);
            if (RandomValue == 1)
            {
                IsShocking = true;
                MyFsm.SetFSMCondition("start_shock", true);
            }

        }
    }
}
