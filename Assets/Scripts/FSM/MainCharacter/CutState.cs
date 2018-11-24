using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutState : IState
{
    private GameObject ThePlayer;
    private FSM MyFsm;
    private CharacterController MyCharacterController;
    private Animator MyAnimator;

    private float TimeInCut;
    private float MaxTimeInCut = 0.8f;

    public void OnEnterState()
    {
        if (ThePlayer == null)
        {
            ThePlayer = GameObject.Find("Player");
        }

        if (MyFsm == null)
        {
            MyFsm = ThePlayer.GetComponent<FSM>();
        }

        if (MyCharacterController == null)
        {
            MyCharacterController = ThePlayer.GetComponent<CharacterController>();
        }

        if (MyAnimator == null)
        {
            MyAnimator = ThePlayer.GetComponent<Animator>();
        }

        AnimatorStateInfo animationState = MyAnimator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] myAnimatorClip = MyAnimator.GetCurrentAnimatorClipInfo(0);
        TimeInCut = myAnimatorClip[0].clip.length - 0.3f;
        Debug.Log(TimeInCut);
    }

    public void OnExitState()
    {
        MyAnimator.SetBool("Is_cutting", false);
    }

    public void UpdateState()
    {
        if (TimeInCut <= 0.0f)
        {
            MyFsm.SetFSMCondition("is_cutting", false);
        }
        else
        {
            TimeInCut -= Time.deltaTime;
        }
    }
}
