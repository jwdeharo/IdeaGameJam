using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState, IState
{
    private List<Vector3> Waypoints;
    private int WayPointIndex;
    private int InitWayPoint;
    private EnemyController MyEnemyController;
    private FSM MyFsm;

    private bool HasStartedThePatrol;

    public void OnEnterState()
    {
        MyFsm = MyGameObject.GetComponent<FSM>();
        MyEnemyController = MyGameObject.GetComponent<EnemyController>();
        WayPointIndex = Random.Range(0, Waypoints.Count);
        InitWayPoint = WayPointIndex;
        HasStartedThePatrol = false;
    }

    public void OnExitState()
    {

    }

    public void UpdateState()
    {
        Vector3 CurrentWaypoint = Waypoints[WayPointIndex];
        Vector3 DistanceToWaypoint = CurrentWaypoint - MyGameObject.transform.position;
        float MoveSpeed = MyEnemyController.GetMoveSpeed();
        while (MoveSpeed > 0.0f)
        {
            if (DistanceToWaypoint.sqrMagnitude > (MoveSpeed * MoveSpeed))
            {
                DistanceToWaypoint.Normalize();
                DistanceToWaypoint.z = 0.0f;
                MyEnemyController.Move(DistanceToWaypoint);
                MoveSpeed = 0.0f;
            }
            else
            {
                MoveSpeed -= DistanceToWaypoint.magnitude;
                if (WayPointIndex < Waypoints.Count - 1)
                {
                    ++WayPointIndex;
                }
                else
                {
                    WayPointIndex = 0;
                }

                HasStartedThePatrol = true;
            }
        }

        if (HasStartedThePatrol && InitWayPoint == WayPointIndex)
        {
            MyFsm.SetFSMCondition("start_patrol", false);
        }
    }

    public void SetWayPoints(Vector3 aWaypoint)
    {
        if (Waypoints == null)
        {
            Waypoints = new List<Vector3>();

        }
        Waypoints.Add(aWaypoint);
    }
}
