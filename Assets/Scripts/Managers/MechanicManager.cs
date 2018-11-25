using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicManager : MonoBehaviour
{

    //Enum to know what mechanics are active at the moments
    public enum E_MECHANICS
    {
        DASH = 0,
        CUT ,
        CHARGE_TELEPORT ,
        SHOOT,


        NUM_MECHANICS
    }

    private const int ActivableMechanics = 2;

    private E_MECHANICS[] MyMechanics;
    private E_MECHANICS[] ActiveMechanics;
    private int CurrentLeftMechanic;
    private int CurrentRighttMechanic;


    // Use this for initialization
    void Start ()
    {
        MyMechanics             = new E_MECHANICS[(int)E_MECHANICS.NUM_MECHANICS];
        
        for (int i = 0; i < (int)E_MECHANICS.NUM_MECHANICS; ++i)
        {
            MyMechanics[i] = (E_MECHANICS)i;
        }

        ActiveMechanics         = new E_MECHANICS[2];
        ActiveMechanics[0]      = E_MECHANICS.DASH;
        ActiveMechanics[1]      = E_MECHANICS.CHARGE_TELEPORT;
        CurrentLeftMechanic     = 0;
        CurrentRighttMechanic   = 3;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (MyMechanics[CurrentRighttMechanic] == ActiveMechanics[0])
        {
            UpdateRightMechanic();
        }

        if (MyMechanics[CurrentLeftMechanic] == ActiveMechanics[1])
        {
            UpdateLeftMechanic();
        }

        ActiveMechanics[0] = MyMechanics[CurrentLeftMechanic];
        ActiveMechanics[1] = MyMechanics[CurrentRighttMechanic];
    }

    public E_MECHANICS[] GetMyMechanics()
    {
        return ActiveMechanics;
    }

    public void UpdateLeftMechanic()
    {
        if (CurrentLeftMechanic < MyMechanics.Length - 1)
        {
            CurrentLeftMechanic++;
        }
        else
        {
            CurrentLeftMechanic = 0;
        }
    }

    public void UpdateRightMechanic()
    {
        if (CurrentRighttMechanic < MyMechanics.Length - 1)
        {
            CurrentRighttMechanic++;
        }
        else
        {
            CurrentRighttMechanic = 0;
        }
    }
}
