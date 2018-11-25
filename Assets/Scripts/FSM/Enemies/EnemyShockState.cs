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
    private bool HasBeenStunned;
    private float Timer;
    private GameObject ThePlayer;

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

        if (ThePlayer == null)
        {
            ThePlayer = GameObject.Find("Player");
        }

        MyAnimator.SetBool("Is_shock", true);
        ToTime = 0.0f;
        Timer = 0.0f;
        FirstTime = true;
        HasBeenStunned = false;
    }

    public void OnExitState()
    {
        MyFsm.PopState();
    }

    public void UpdateState()
    {
        Vector3 DistanceToWaypoint = ThePlayer.transform.position - MyGameObject.transform.position;

        if (!HasBeenStunned && DistanceToWaypoint.magnitude < 2.0f)
        {
            FSM PlayerFSM = ThePlayer.GetComponent<FSM>();

            if (!PlayerFSM.IsState("Dash"))
            {
                PlayerFSM.SetFSMCondition("is_stunned", true);
                HasBeenStunned = true;
                Timer = 2.0f;
            }
        }
        else if (HasBeenStunned)
        {
            FSM PlayerFSM = ThePlayer.GetComponent<FSM>();

            if (!PlayerFSM.IsState("Stunned"))
            {
                if (Timer <= 0.0f)
                {
                    HasBeenStunned = false;
                }
                else
                {
                    Timer -= Time.deltaTime;
                }
            }
        }
        
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
