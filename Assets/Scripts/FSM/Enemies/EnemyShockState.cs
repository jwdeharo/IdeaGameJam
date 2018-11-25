using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShockState : EnemyBaseState, IState
{
    private FSM MyFsm;
    private Animator MyAnimator;

    public void OnEnterState()
    {
        if (MyAnimator == null)
        {
            MyAnimator = MyGameObject.GetComponent<Animator>();
        }

        if (MyFsm == null)
        {
            MyFsm = MyGameObject.GetComponent<FSM>();
        }
        
        MyAnimator.SetBool("Is_shock", true);
    }

    public void OnExitState()
    {

    }

    public void UpdateState()
    {
        //MyFsm.SetFSMCondition("start_shock", false);
    }
}
