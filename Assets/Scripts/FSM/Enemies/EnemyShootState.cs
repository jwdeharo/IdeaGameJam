using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootState : EnemyBaseState, IState {

    private const float TIME_TO_SHOOT = 0.25f;

    private float timeRemaining;

    private EnemyController MyController;
    private FSM MyFsm;
    private GameObject ThePlayer;
    private GameObject bullet;
    private string condition;

    public void OnEnterState()
    {
        bullet = (GameObject)Resources.Load("EnemyProjectile");

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

        timeRemaining = TIME_TO_SHOOT;
    }

    public void OnExitState()
    {
        MyFsm.PopState();
    }

    public void UpdateState()
    {
        if (timeRemaining <= 0.0f)
        {
            MyFsm.SetFSMCondition(condition, false);
            Vector3 PlayerPosition = ThePlayer.transform.position;

            Vector3 DirectionOfShooting = (PlayerPosition - MyGameObject.transform.position).normalized;
            MyController.Shoot(bullet, DirectionOfShooting);
            MyFsm.SetFSMCondition("is_shooting", false);
            Debug.Log("bitch");
        }
        else
        {
            timeRemaining -= Time.deltaTime;
        }
    }
}
