using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyBaseState, IState
{
    private FSM MyFsm;
    private Animator MyAnimator;
    private float TimeInDead;

    public void OnEnterState()
    {
        if (MyFsm == null)
        {
            MyFsm = MyGameObject.GetComponent<FSM>();
        }

        if (MyAnimator == null)
        {
            MyAnimator = MyGameObject.GetComponent<Animator>();
        }

        MyAnimator.SetBool("time_to_die", true);
        AnimatorStateInfo animationState = MyAnimator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] myAnimatorClip = MyAnimator.GetCurrentAnimatorClipInfo(0);
        TimeInDead = myAnimatorClip[0].clip.length - 0.3f;
    }

    public void OnExitState()
    {
        MyFsm.PopState();
    }

    public void UpdateState()
    {
        if (TimeInDead <= 0.0f)
        {
            EnemyController MyController = MyGameObject.GetComponent<EnemyController>();
            MyFsm.PopState();
            MyAnimator.SetBool("time_to_die", false);
            MyController.DestroyMe(MyGameObject);
            
        }
        else
        {
            TimeInDead -= Time.deltaTime;
        }
    }
}
