using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicManager : MonoBehaviour
{

    //Enum to know what mechanics are active at the moments
    public enum E_MECHANICS: int
    {
        DASH = 0,
        CUT ,
        CHARGE_TELEPORT ,
        SHOOT,
        SHOOT_SLOW,


        NUM_MECHANICS
    }

    private const int ActivableMechanics = 2;

    private E_MECHANICS[] MyMechanics;
    public E_MECHANICS[] ActiveMechanics;
    public List<E_MECHANICS> UnlockedMechanics;
    public int CurrentLeftMechanic;
    public int CurrentRighttMechanic;


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
        UnlockedMechanics = new List<E_MECHANICS>();
        UnlockedMechanics.Add(E_MECHANICS.DASH);
        CurrentLeftMechanic     = 0;
        CurrentRighttMechanic   = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (UnlockedMechanics.Count > 1)
        {
            if (UnlockedMechanics[CurrentRighttMechanic] == ActiveMechanics[0])
            {
                UpdateRightMechanic();
            }

            if (UnlockedMechanics[CurrentLeftMechanic] == ActiveMechanics[1])
            {
                UpdateLeftMechanic();
            }
        }

        ActiveMechanics[0] = UnlockedMechanics[CurrentLeftMechanic];
        ActiveMechanics[1] = UnlockedMechanics[CurrentRighttMechanic];
    }

    public E_MECHANICS[] GetMyMechanics()
    {
        return ActiveMechanics;
    }

    public void UpdateLeftMechanic()
    {
        if (CurrentLeftMechanic < UnlockedMechanics.Count - 1)
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
        if (CurrentRighttMechanic < UnlockedMechanics.Count - 1)
        {
            CurrentRighttMechanic++;
        }
        else
        {
            CurrentRighttMechanic = 0;
        }
    }
}
