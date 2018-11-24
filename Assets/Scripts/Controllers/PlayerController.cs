using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Enum to know what mechanics are active at the moments
    private enum E_MECHANICS
    {
        DASH = 0,

        NUM_MECHANICS
    }

    //My finite state machine.
    private FSM MyFsmMachine;
    //Character controller that will help us to move and detect collisions.
    private CharacterController MyController;

    private IdleState MyIdleState;
    private MoveState MyMoveState;
    private DashState MyDashState;
    private Vector3 MyDirection;
    private E_MECHANICS[] MyMechanics;
    public float MoveSpeed = 5.0f;

    // Use this for initialization
    void Start()
    {
        MyController = GetComponent<CharacterController>();
        MyFsmMachine = GetComponent<FSM>();

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

        MyMechanics = new E_MECHANICS[(int)E_MECHANICS.NUM_MECHANICS];
        MyMechanics[0] = E_MECHANICS.DASH;

        MyDirection = Vector3.zero;
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
            switch (MyMechanics[0])
            {
                case E_MECHANICS.DASH:
                    MyFsmMachine.SetFSMCondition("is_dashing", true);
                    break;
            }
        }

    }

    // Each state will call this function and will move according its characteristics.
    public void Move(Vector3 aMovement)
    {
        //This should do the trick.
        MyController.Move(aMovement * Time.deltaTime * MoveSpeed);
    }

    public void SetDirection(Vector3 aDirection)
    {
        MyDirection = aDirection;
    }

    public Vector3 GetDirection()
    {
        return MyDirection;
    }

}
