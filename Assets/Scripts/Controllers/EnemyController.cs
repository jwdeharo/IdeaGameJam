using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private EnemyIdleState MyIdleState;
    private EnemyPatrolState MyPatrolState;

    private CharacterController MyController;
    private FSM MyFsm;
    public GameObject Me;

    public float MoveSpeed;
    public string Name;

    // Use this for initialization
    void Start()
    {
        MyFsm = GetComponent<FSM>();
        MyController = GetComponent<CharacterController>();

        //Init of the enemy states.
        MyIdleState = new EnemyIdleState();
        MyPatrolState = new EnemyPatrolState();

        MyIdleState.SetMyGameObject(Me);
        MyPatrolState.SetMyGameObject(Me);

        for (int ChildIndex = 0; ChildIndex < transform.childCount; ChildIndex++)
        {
            Transform Child = transform.GetChild(ChildIndex);
            MyPatrolState.SetWayPoints(Child.position);
        }

        CCondition IdleToPatrol = new CCondition("start_patrol", MyPatrolState, true, false);
        CCondition PatrolToIdle = new CCondition("start_patrol", MyIdleState, false, false);

        MyFsm.AddState("Idle", MyIdleState);
        MyFsm.AddState("Patrol", MyPatrolState);

        MyFsm.AddCondition(MyIdleState, IdleToPatrol);
        MyFsm.AddCondition(MyPatrolState, PatrolToIdle);
    }

    // Update is called once per frame
    void Update()
    {
        //In the idle state we wait to start patrolling.

    }

    // Each state will call this function and will move according its characteristics.
    public void Move(Vector3 aMovement, bool aIsDashing = false)
    {
        MyController.Move(aMovement * Time.deltaTime * MoveSpeed);
        // Flip((aMovement).normalized.x);
    }

    public float GetMoveSpeed()
    {
        return MoveSpeed * Time.deltaTime;
    }
}
