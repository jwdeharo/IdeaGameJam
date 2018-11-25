﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private EnemyIdleState MyIdleState;
    private EnemyPatrolState MyPatrolState;
    private EnemyChaseState MyChaseState;

    internal void Slow(object tIME)
    {
        throw new NotImplementedException();
    }

    private EnemyShockState MyShockState;

    private CharacterController MyController;
    private FSM MyFsm;
    public GameObject Me;

    public float MoveSpeed;
    public string Name;

    private float FreezeRemaining;
    private bool freezed;

    // Use this for initialization
    void Start()
    {
        MyFsm = GetComponent<FSM>();
        MyController = GetComponent<CharacterController>();

        //Init of the enemy states.
        MyIdleState = new EnemyIdleState();
        MyPatrolState = new EnemyPatrolState();
        MyChaseState = new EnemyChaseState();
        MyShockState = new EnemyShockState();

        MyIdleState.SetMyGameObject(Me);
        MyPatrolState.SetMyGameObject(Me);
        MyChaseState.SetMyGameObject(Me);
        MyShockState.SetMyGameObject(Me);

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

        MyFsm.AddState("Idle", MyIdleState);
        MyFsm.AddState("Patrol", MyPatrolState);
        MyFsm.AddState("Chase", MyPatrolState);
        MyFsm.AddState("Shock", MyChaseState);

        MyFsm.AddCondition(MyIdleState, IdleToPatrol);
        MyFsm.AddCondition(MyIdleState, IdleToChase);
        MyFsm.AddCondition(MyPatrolState, PatrolToIdle);
        MyFsm.AddCondition(MyPatrolState, PatrolToChase);
        MyFsm.AddCondition(MyChaseState, ChaseToIdle);
        MyFsm.AddCondition(MyChaseState, ChaseToShock);
        MyFsm.AddCondition(MyShockState, ShockToChase);
    }

    // Update is called once per frame
    void Update()
    {
        if(freezed)
        {
            FreezeRemaining -= Time.deltaTime;
            Debug.Log(FreezeRemaining);
            if(FreezeRemaining < 0)
            {
                freezed = false;
                MoveSpeed = MoveSpeed * 2;
            }
        }

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

    public void Slow(float timeOfFreeze)
    {
        if (!freezed)
        {
            freezed = true;
            MoveSpeed = MoveSpeed / 2;
        }
        FreezeRemaining = timeOfFreeze;

    }

    public void AnimationHasEnded()
    {
        Debug.Log("SHOCKSHOCK");
    }
}
