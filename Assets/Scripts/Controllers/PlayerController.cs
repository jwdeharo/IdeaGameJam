using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //My finite state machine.
    private FSM MyFsmMachine;
    //Character controller that will help us to move and detect collisions.
    private CharacterController MyController;

    private IdleState MyIdleState;
    private MoveState MyMoveState;
    private DashState MyDashState;
    private Vector3 MyDirection;
    private MechanicManager MyMechanicManager;
    private bool FacingRight;
    private float SensibilityTrigger;

    public float MoveSpeed = 5.0f;
    public Animator MyAnimator;


    // Use this for initialization
    void Start()
    {
        MyController = GetComponent<CharacterController>();
        MyFsmMachine = GetComponent<FSM>();
        MyAnimator = GetComponent<Animator>();
        MyMechanicManager = GetComponent<MechanicManager>();

        ////We start the states here.
        MyIdleState = new IdleState();
        MyMoveState = new MoveState();
        MyDashState = new DashState();

        //We define conditions to change between states here.
        CCondition IdleToMove = new CCondition("is_moving", MyMoveState, true, false);
        CCondition MoveToIdle = new CCondition("is_moving", MyIdleState, false, false);
        CCondition IdleToDash = new CCondition("is_dashing", MyDashState, true, false);
        CCondition DashToIdle = new CCondition("is_dashing", MyIdleState, false, false);
        CCondition MoveToDash = new CCondition("is_dashing", MyDashState, true, false);

        MyFsmMachine.AddState("Idle", MyIdleState);
        MyFsmMachine.AddState("Move", MyMoveState);
        MyFsmMachine.AddState("Dash", MyDashState);

        //This relates the states with their conditions.
        MyFsmMachine.AddCondition(MyIdleState, IdleToMove);
        MyFsmMachine.AddCondition(MyIdleState, IdleToDash);
        MyFsmMachine.AddCondition(MyMoveState, MoveToIdle);
        MyFsmMachine.AddCondition(MyMoveState, MoveToDash);
        MyFsmMachine.AddCondition(MyDashState, DashToIdle);

        MyDirection = Vector3.zero;
        FacingRight = true;
        SensibilityTrigger = 0.0f;
    }

    private void FixedUpdate()
    {
        //If the input is different from 0, then this means that we're moving.
        if (InputManager.GetJoystickMovement() != Vector3.zero && !MyFsmMachine.IsState("Dash"))
        {
            MyFsmMachine.SetFSMCondition("is_moving", true);
        }
        else
        {
            MyFsmMachine.SetFSMCondition("is_moving", false);
        }

        if (InputManager.FirstMechanicPressed())
        {
            switch (MyMechanicManager.GetMyMechanics()[0])
            {
                case MechanicManager.E_MECHANICS.DASH:
                    MyFsmMachine.SetFSMCondition("is_dashing", true);
                    break;
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

    // Each state will call this function and will move according its characteristics.
    public void Move(Vector3 aMovement, bool aIsDashing = false)
    {
        //This should do the trick.
        float MovementSpeed = 0.0f;

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

        MyController.Move(aMovement * Time.deltaTime * MoveSpeedWithDash);

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

}
