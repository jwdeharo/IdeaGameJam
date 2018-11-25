using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState, IState
{
    public float TimeToWait = 5.0f;
    
    private FSM MyFsm;
    private float TimeWaiting;
    
    public void OnEnterState()
    {
        TimeWaiting = TimeToWait;

        if (MyFsm == null)
        {
            MyFsm = MyGameObject.GetComponent<FSM>();
        }

    }


    public void OnExitState()
    {
        TimeWaiting = TimeToWait;

    }

    public void UpdateState()
    {
        if (TimeWaiting <= 0.0f)
        {
            
            MyFsm.SetFSMCondition("start_patrol", true);
        }
        else
        {
            TimeWaiting -= Time.deltaTime;
        }
    }
}
