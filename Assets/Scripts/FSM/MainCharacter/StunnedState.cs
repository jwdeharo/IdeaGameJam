using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : IState
{
    private FSM MyFsm;
    private Animator MyAnimator;
    private GameObject ThePlayer;

    private float MaxTimer;
    private float Timer;

    public void OnEnterState()
    {
        if (ThePlayer == null)
        {
            ThePlayer = GameObject.Find("Player");
        }

        if (MyAnimator == null)
        {
            MyAnimator = ThePlayer.GetComponent<Animator>();
        }

        if (MyFsm == null)
        {
            MyFsm = ThePlayer.GetComponent<FSM>();
        }

        MaxTimer = 2.0f;
        Timer = MaxTimer;
        MyAnimator.SetBool("stunned", true);
    }

    public void OnExitState()
    {
        MyFsm.PopState();
        MyAnimator.SetBool("stunned", false);
    }

    public void UpdateState()
    {
        if (Timer <= 0.0f)
        {
            MyFsm.SetFSMCondition("is_stunned", false);
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }
}
