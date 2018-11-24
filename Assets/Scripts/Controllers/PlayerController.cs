using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //My finite state machine.
    private FSM MyFsmMachine;
    //Character controller that will help us to move and detect collisions.
    private CharacterController MyController;

    public IdleState MyIdleState;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //The update of the player controller is the one that will handle inputs.
        Move(InputManager.GetJoystickMovement());
	}

    // Each state will call this function and will move according its characteristics.
    public void Move(Vector3 aMovement)
    {
        //This should to the trick.
        MyController.Move(aMovement * Time.deltaTime * 2.0f);
    }
}
