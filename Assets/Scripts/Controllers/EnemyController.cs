using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private EnemyIdleState MyIdleState;
    private FSM MyFsm;
    
	// Use this for initialization
	void Start ()
    {
        MyFsm       = GetComponent<FSM>();

        //Init of the enemy states.
        MyIdleState = new EnemyIdleState();

        MyFsm.AddState("Idle", MyIdleState);
    }
	
	// Update is called once per frame
	void Update ()
    {
		//In the idle state we wait to start patrolling.

	}
}
