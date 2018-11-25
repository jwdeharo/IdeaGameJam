using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState, IState
{
    private FSM MyFsm;
    private CharacterController MyController;
    private GameObject ThePlayer;
    private EnemyController MyEnemyController;

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
    }

    public void OnExitState()
    {

    }

    public void UpdateState()
    {
        //We calculate the distance to the target.
        Vector3 DistanceToWaypoint = ThePlayer.transform.position - MyGameObject.transform.position;

        MyEnemyController.Move(DistanceToWaypoint);
    }
}
