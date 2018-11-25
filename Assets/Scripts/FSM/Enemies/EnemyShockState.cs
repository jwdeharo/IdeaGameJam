using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShockState : EnemyBaseState, IState
{
    private FSM MyFsm;
    private Animator MyAnimator;
    private float MaxTime;
    private float ToTime;
    private bool FirstTime;

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
        ToTime = 0.0f;
        FirstTime = true;
    }

    public void OnExitState()
    {

    }

    public void UpdateState()
    {
        if (FirstTime && MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shock"))
        {
            MaxTime = MyAnimator.GetCurrentAnimatorStateInfo(0).length;
            ToTime = MaxTime;
            FirstTime = false;
        }

        if (ToTime <= 0.0f)
        {
            MyAnimator.SetBool("Is_shock", false);
            MyFsm.SetFSMCondition("start_shock", false);
        }
        else
        {
            ToTime -= Time.deltaTime;
        }
    }
}
