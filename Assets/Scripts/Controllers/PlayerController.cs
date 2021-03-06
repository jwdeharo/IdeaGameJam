using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{


    //My finite state machine.
    private FSM MyFsmMachine;
    //Character controller that will help us to move and detect collisions.
    private Rigidbody2D MyRigidBody;

    private IdleState MyIdleState;
    private MoveState MyMoveState;
    private DashState MyDashState;
    private CutState MyCutState;
    private StunnedState MyStunnedState;

    private Vector3 MyDirection;
    private MechanicManager MyMechanicManager;
    private GameObject ToCut;

    private bool FacingRight;
    private bool CanCutEnemy;
    private float SensibilityTrigger;

    public float MoveSpeed = 5.0f;
    public Animator MyAnimator;
    public bool CanMove;
    private float Timer;


    // Use this for initialization
    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
        MyFsmMachine = GetComponent<FSM>();
        MyAnimator = GetComponent<Animator>();
        MyMechanicManager = GetComponent<MechanicManager>();

        ////We start the states here.
        MyIdleState = new IdleState();
        MyMoveState = new MoveState();
        MyDashState = new DashState();
        MyCutState = new CutState();
        MyStunnedState = new StunnedState();
        //MyShootState = new ShootState();
        //We define conditions to change between states here.
        
        CCondition IdleToMove = new CCondition("is_moving", MyMoveState, true, false);
        CCondition MoveToIdle = new CCondition("is_moving", MyIdleState, false, false);

        //CCondition IdleToShoot = new CCondition("is_shooting", MyShootState, true, false);
        //CCondition ShootToIdle = new CCondition("is_shooting", MyIdleState, false, false);

        CCondition IdleToDash = new CCondition("is_dashing", MyDashState, true, false);
        CCondition DashToIdle = new CCondition("is_dashing", MyIdleState, false, false);
        CCondition MoveToDash = new CCondition("is_dashing", MyDashState, true, false);
        CCondition IdleToCut = new CCondition("is_cutting", MyCutState, true, false);
        CCondition CutToIdle = new CCondition("is_cutting", MyIdleState, false, false);
        CCondition CutToMove = new CCondition("is_cutting", MyMoveState, false, false);
        CCondition MoveToCut = new CCondition("is_cutting", MyCutState, true, false);
        CCondition IdleToStunned = new CCondition("is_stunned", MyStunnedState, true, false);
        CCondition MoveToStunned = new CCondition("is_stunned", MyStunnedState, true, false);
        CCondition CutToStunned = new CCondition("is_stunned", MyStunnedState, true, false);
        CCondition StunnedToIdle = new CCondition("is_stunned", MyIdleState, false, false);

        MyFsmMachine.AddState("Idle", MyIdleState);
        MyFsmMachine.AddState("Move", MyMoveState);
        MyFsmMachine.AddState("Dash", MyDashState);
        MyFsmMachine.AddState("Cut", MyCutState);
        MyFsmMachine.AddState("Stunned", MyStunnedState);
        //MyFsmMachine.AddState("Shoot", MyShootState);

        //This relates the states with their conditions.
        MyFsmMachine.AddCondition(MyIdleState, IdleToMove);
        MyFsmMachine.AddCondition(MyIdleState, IdleToDash);
        MyFsmMachine.AddCondition(MyIdleState, IdleToCut);
        MyFsmMachine.AddCondition(MyIdleState, IdleToStunned);

        MyFsmMachine.AddCondition(MyMoveState, MoveToIdle);
        MyFsmMachine.AddCondition(MyMoveState, MoveToDash);
        MyFsmMachine.AddCondition(MyMoveState, MoveToCut);
        MyFsmMachine.AddCondition(MyMoveState, MoveToStunned);

        MyFsmMachine.AddCondition(MyDashState, DashToIdle);
        MyFsmMachine.AddCondition(MyCutState, CutToIdle);
        MyFsmMachine.AddCondition(MyStunnedState, StunnedToIdle);

        MyFsmMachine.AddCondition(MyCutState, CutToStunned);

        MyDirection = Vector3.zero;
        FacingRight = true;
        CanCutEnemy = false;
        CanMove = true;
        SensibilityTrigger = 0.0f;
        Timer = 0.0f;
    }
    
    private void FixedUpdate()
    {
        if (DataManager.CurrentMechanic != MechanicManager.E_MECHANICS.NONE_MECHANIC)
        {
            MyMechanicManager.UnlockMechanic(DataManager.CurrentMechanic);
            DataManager.CurrentMechanic = MechanicManager.E_MECHANICS.NONE_MECHANIC;
        }

        //If the input is different from 0, then this means that we're moving.
        if (InputManager.GetJoystickMovement() != Vector3.zero && !MyFsmMachine.IsState("Dash") && !MyFsmMachine.IsState("Cut") && !MyFsmMachine.IsState("Stunned") && !MyFsmMachine.IsState("Shoot"))
        {
            MyFsmMachine.SetFSMCondition("is_moving", true);
        }
        else
        {
            MyFsmMachine.SetFSMCondition("is_moving", false);
        }

        //MyFsmMachine.SetFSMCondition("is_shooting", true);

        if (InputManager.FirstMechanicPressed())
        {
            if (!MyFsmMachine.IsState("Stunned") && !MyFsmMachine.IsState("Dash") && !MyFsmMachine.IsState("Cut"))
            {
                ActivateMechanic(0);
            }
        }
        else if (InputManager.SecondMechanicPressed())
        {
            if (!MyFsmMachine.IsState("Stunned") && !MyFsmMachine.IsState("Dash") && !MyFsmMachine.IsState("Cut"))
            {
                ActivateMechanic(1);
            }
        }

        if (SensibilityTrigger == 0.0f && InputManager.ChangeMechanic(ref SensibilityTrigger))
        {
            if (SensibilityTrigger < 0.0f)
            {
                MyMechanicManager.UpdateRightMechanic();
            }
            else
            {
                MyMechanicManager.UpdateLeftMechanic();
            }
        }
        else
        {
            InputManager.ChangeMechanic(ref SensibilityTrigger);
        }

    }

    private void ActivateMechanic(int aMechanicIndex)
    {
        
        MyMechanicManager.MechanicUsed(MechanicManager.GetCurrentMechanic(aMechanicIndex));
        switch (MyMechanicManager.GetMyMechanics()[aMechanicIndex])
        {
            case MechanicManager.E_MECHANICS.DASH:
                Debug.Log("DAASH");
                MyFsmMachine.SetFSMCondition("is_dashing", true);
                break;
            case MechanicManager.E_MECHANICS.CUT:
                MyFsmMachine.SetFSMCondition("is_cutting", true);
                if (ToCut != null)
                {
                    FSM EnemyFsm = ToCut.GetComponent<FSM>();

                    if (EnemyFsm != null)
                    {
                        EnemyFsm.SetFSMCondition("time_to_die", true);
                    }
                }
                break;
        }
    }

    // Each state will call this function and will move according its characteristics.
    public void Move(Vector3 aMovement, bool aIsDashing = false)
    {
        //This should do the trick.
        float MovementSpeed = 0.0f;
        Vector2 NewMovement = new Vector2(aMovement.x, aMovement.y);
        MovementSpeed = aMovement.x != 0 ? aMovement.x : aMovement.y;

        float MoveSpeedWithDash = MoveSpeed;

        if (!aIsDashing)
        {
            MyAnimator.SetFloat("Speed", Mathf.Abs(MovementSpeed));
        }
        else
        {
            MoveSpeedWithDash = 1.0f;
        }

        transform.Translate(NewMovement * Time.deltaTime * MoveSpeedWithDash);

        Flip((aMovement).normalized.x);
    }

    public void SetDirection(Vector3 aDirection)
    {
        MyDirection = aDirection;
    }

    public Vector3 GetDirection()
    {
        return MyDirection;
    }

    private void Flip(float aFlipX)
    {
        if ((aFlipX > 0.0f && !FacingRight) || (aFlipX < 0.0f && FacingRight))
        {
            Vector3 MyScale = transform.localScale;

            MyScale.x *= -1.0f;

            transform.localScale = MyScale;

            FacingRight = !FacingRight;
        }
    }

    public Animator GetMyAnimator()
    {
        return MyAnimator;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.parent != null)
        {
            string ColTag = col.gameObject.transform.parent.gameObject.tag;
            if (ColTag == "Enemy")
            {
                ToCut = col.gameObject.transform.parent.gameObject;
                CanCutEnemy = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        string ColTag = col.gameObject.transform.parent.gameObject.tag;
        if (ColTag == "Enemy")
        {
            ToCut = null;
            CanCutEnemy = false;
        }
    }

    public bool CanCutEnemyFunc()
    {
        return CanCutEnemy;
    }

    public GameObject GetToCut()
    {
        return ToCut;
    }
}