﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private EnemyIdleState MyIdleState;
    private EnemyPatrolState MyPatrolState;
    private EnemyChaseState MyChaseState;
    private EnemyShockState MyShockState;
    private EnemyDieState MyDieState;
    private EnemyDashState MyDashState;

    private CharacterController MyController;
    private FSM MyFsm;
    private MechanicManager PlayerMechanics;

    private List<MechanicManager.E_MECHANICS> CopiedMechanics;

    public GameObject Me;

    public float MoveSpeed;
    public string Name;
    public int TimesToCopy;

    // Use this for initialization
    void Start()
    {
        GameObject ThePlayer = GameObject.Find("Player");

        MyFsm = GetComponent<FSM>();
        MyController = GetComponent<CharacterController>();
        PlayerMechanics = ThePlayer.GetComponent<MechanicManager>();

        CopiedMechanics = new List<MechanicManager.E_MECHANICS>();

        //Init of the enemy states.
        MyIdleState = new EnemyIdleState();
        MyPatrolState = new EnemyPatrolState();
        MyChaseState = new EnemyChaseState();
        MyShockState = new EnemyShockState();
        MyDieState = new EnemyDieState();
        MyDashState = new EnemyDashState();

        MyIdleState.SetMyGameObject(Me);
        MyPatrolState.SetMyGameObject(Me);
        MyChaseState.SetMyGameObject(Me);
        MyShockState.SetMyGameObject(Me);
        MyDieState.SetMyGameObject(Me);
        MyDashState.SetMyGameObject(Me);

        for (int ChildIndex = 0; ChildIndex < transform.childCount; ChildIndex++)
        {
            Transform Child = transform.GetChild(ChildIndex);
            MyPatrolState.SetWayPoints(Child.position);
        }

        CCondition IdleToPatrol = new CCondition("start_patrol", MyPatrolState, true, false);
        CCondition IdleToChase = new CCondition("start_chasing", MyChaseState, true, false);
        CCondition PatrolToIdle = new CCondition("start_patrol", MyIdleState, false, false);
        CCondition PatrolToChase = new CCondition("start_chasing", MyChaseState, true, false);
        CCondition ChaseToIdle = new CCondition("start_chasing", MyIdleState, false, false);
        CCondition ChaseToShock = new CCondition("start_shock", MyShockState, true, false);
        CCondition ShockToChase = new CCondition("start_shock", MyChaseState, false, false);
        CCondition PatrolToDieState = new CCondition("time_to_die", MyDieState, true, false);
        CCondition IdleToDieState = new CCondition("time_to_die", MyDieState, true, false);
        CCondition ShockToDieState = new CCondition("time_to_die", MyDieState, true, false);
        CCondition ChaseToDieState = new CCondition("time_to_die", MyDieState, true, false);
        CCondition IdleToDash = new CCondition("is_dashing", MyDashState, true, false);
        CCondition PatrolToDash = new CCondition("is_dashing", MyDashState, true, false);
        CCondition ChaseToDash = new CCondition("is_dashing", MyDashState, true, false);
        CCondition DashToChase = new CCondition("is_dashing", MyChaseState, false, false);

        MyFsm.AddState("Idle", MyIdleState);
        MyFsm.AddState("Patrol", MyPatrolState);
        MyFsm.AddState("Chase", MyPatrolState);
        MyFsm.AddState("Shock", MyChaseState);
        MyFsm.AddState("Die", MyDieState);

        MyFsm.AddCondition(MyIdleState, IdleToPatrol);
        MyFsm.AddCondition(MyIdleState, IdleToChase);
        MyFsm.AddCondition(MyIdleState, IdleToDash);
        MyFsm.AddCondition(MyPatrolState, PatrolToIdle);
        MyFsm.AddCondition(MyPatrolState, PatrolToChase);
        MyFsm.AddCondition(MyPatrolState, PatrolToDieState);
        MyFsm.AddCondition(MyPatrolState, PatrolToDash);
        MyFsm.AddCondition(MyChaseState, ChaseToIdle);
        MyFsm.AddCondition(MyChaseState, ChaseToShock);
        MyFsm.AddCondition(MyChaseState, ChaseToDieState);
        MyFsm.AddCondition(MyChaseState, ChaseToDash);
        MyFsm.AddCondition(MyShockState, ShockToChase);
        MyFsm.AddCondition(MyIdleState, IdleToDieState);
        MyFsm.AddCondition(MyShockState, ShockToDieState);
        MyFsm.AddCondition(MyDashState, DashToChase);
    }

    // Update is called once per frame
    void Update()
    {
        //In the idle state we wait to start patrolling.
        if (!CopiedMechanics.Contains(PlayerMechanics.GetMoreUsedMechanic()) && PlayerMechanics.GetIntMoreUsedMechanic() >= TimesToCopy)
        {
            CopiedMechanics.Add(PlayerMechanics.GetMoreUsedMechanic());
        }

        if (CopiedMechanics.Count > 0)
        {
            int RandomIndex = Random.Range(0, CopiedMechanics.Count);
            int RandomValue = Random.Range(0, 10000);

            if (RandomValue > 9900)
            {
                switch (CopiedMechanics[RandomIndex])
                {
                    case MechanicManager.E_MECHANICS.DASH:
                        Debug.Log("Dash");
                        MyFsm.SetFSMCondition("is_dashing", true);
                        break;
                    case MechanicManager.E_MECHANICS.CUT:
                        Debug.Log("USing Cut");
                        break;
                }
            }
        }
    }

    // Each state will call this function and will move according its characteristics.
    public void Move(Vector3 aMovement, bool aIsDashing = false)
    {
        float RealMoveSpeed = MoveSpeed;

        if (aIsDashing)
        {
            RealMoveSpeed = 1.0f;
        }

        MyController.Move(aMovement * Time.deltaTime * RealMoveSpeed);
        // Flip((aMovement).normalized.x);
    }

    public float GetMoveSpeed()
    {
        return MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider col)
    {
        GameObject MyParent = col.gameObject.transform.parent.gameObject;
        if (MyParent.tag == "Player")
        {
            MechanicManager PlayerMechanics = MyParent.GetComponent<MechanicManager>();

            if (PlayerMechanics.GetMyMechanics().Length > 1)
            {
                MyFsm.SetFSMCondition("start_chasing", true);
            }
        }
    }

    public void DestroyMe(GameObject aToDestroy)
    {
        Destroy(aToDestroy);
    }
}

