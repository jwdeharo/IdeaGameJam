using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //My finite state machine.
    private FSM MyFsmMachine;
    //Character controller that will help us to move and detect collisions.
    private CharacterController MyController;

    public float MoveSpeed = 5.0f;
    private IdleState MyIdleState;
    private MoveState MyMoveState;
	// Use this for initialization
	void Start ()
    {
        MyController = GetComponent<CharacterController>();
        MyFsmMachine = GetComponent<FSM>();

        ////We start the states here.
        MyIdleState = new IdleState();
        MyMoveState = new MoveState();
        
        CCondition IdleToMove = new CCondition("is_moving", MyMoveState, true, false);
        CCondition MoveToIdle = new CCondition("is_moving", MyIdleState, false, false);
        
        MyFsmMachine.AddState("Idle", MyIdleState);
        MyFsmMachine.AddState("Move", MyMoveState);
        MyFsmMachine.AddCondition(MyIdleState, IdleToMove);
        MyFsmMachine.AddCondition(MyMoveState, MoveToIdle);
    }

    // Each state will call this function and will move according its characteristics.
    public void Move(Vector3 aMovement)
    {
        //This should to the trick.
        MyController.Move(aMovement * Time.deltaTime * MoveSpeed);
    }
}
