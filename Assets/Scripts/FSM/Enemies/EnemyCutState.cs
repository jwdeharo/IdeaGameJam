using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCutState : EnemyBaseState, IState
{
    private FSM MyFsm;
    private CharacterController MyCharacterController;
    private EnemyController MyController;
    private Animator MyAnimator;

    private float TimeInCut;
    private float MaxTimeInCut = 0.8f;

    public void OnEnterState()
    {

        if (MyFsm == null)
        {
            MyFsm = MyGameObject.GetComponent<FSM>();
        }

        if (MyCharacterController == null)
        {
            MyCharacterController = MyGameObject.GetComponent<CharacterController>();
        }

        if (MyController == null)
        {
            MyController = MyGameObject.GetComponent<EnemyController>();
        }

        if (MyAnimator == null)
        {
            MyAnimator = MyGameObject.GetComponent<Animator>();
        }

       //MyAnimator.SetBool("Is_cutting", true);
       //AnimatorStateInfo animationState = MyAnimator.GetCurrentAnimatorStateInfo(0);
       //AnimatorClipInfo[] myAnimatorClip = MyAnimator.GetCurrentAnimatorClipInfo(0);
        TimeInCut = 0.3f;
    }

    public void OnExitState()
    {
        MyFsm.PopState();
        //MyAnimator.SetBool("Is_cutting", false);
    }

    public void UpdateState()
    {
        if (MyController.CanCut)
        {
            //we hit the player here.
            if (TimeInCut <= 0.0f)
            {
                MyFsm.SetFSMCondition("is_cutting", false);
            }
            else
            {
                TimeInCut -= Time.deltaTime;
            }
        }
        else
        {
            MyFsm.SetFSMCondition("is_cutting", false);
        }
    }
}
