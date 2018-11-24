using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    public float TimeToWait = 10.0f;
    private float TimeWaiting;

    public void OnEnterState()
    {
        TimeWaiting = TimeToWait;
    }


    public void OnExitState()
    {

    }

    public void UpdateState()
    {
        if (TimeWaiting <= 0.0f)
        {
            Debug.Log("TIME TO START PATROLLING");
            TimeWaiting = TimeToWait;
        }
        else
        {
            TimeWaiting -= Time.deltaTime;
        }
    }
}
