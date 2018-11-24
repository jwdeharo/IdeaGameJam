using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour
{
    private PlayerController MyPlayerController;

    public void OnEnterState()
    {
        //When we enter the state, we find the player's PlayerController.
        MyPlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void UpdateState()
    {
        //The UpdateState does not do much yet. 
    }

    public void OnExitState()
    {

    }
}
