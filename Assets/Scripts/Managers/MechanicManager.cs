using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MechanicManager : MonoBehaviour
{

    //Enum to know what mechanics are active at the moments
    public enum E_MECHANICS
    {
        DASH = 0,
        CUT,

        NONE_MECHANIC,

        NUM_MECHANICS
    }

    private const int ActivableMechanics = 2;

    private E_MECHANICS[] MyMechanics;
    private E_MECHANICS[] ActiveMechanics;
    private Dictionary<E_MECHANICS, int> NumberUsedMechanics;
    private int CurrentLeftMechanic;
    private int CurrentRighttMechanic;


    // Use this for initialization
    void Start ()
    {
        MyMechanics             = new E_MECHANICS[(int)E_MECHANICS.NUM_MECHANICS];
        NumberUsedMechanics     = new Dictionary<E_MECHANICS, int>();


        for (int i = 0; i < (int)E_MECHANICS.NUM_MECHANICS; ++i)
        {
            MyMechanics[i] = E_MECHANICS.NONE_MECHANIC;
            NumberUsedMechanics.Add((E_MECHANICS)i, 0);
        }

        ActiveMechanics         = new E_MECHANICS[2];
        MyMechanics[(int)E_MECHANICS.DASH]          = E_MECHANICS.DASH;
        MyMechanics[(int)E_MECHANICS.CUT] = E_MECHANICS.CUT;
        ActiveMechanics[0]      = E_MECHANICS.DASH;
        ActiveMechanics[1]      = E_MECHANICS.CUT;
        CurrentLeftMechanic     = 0;
        CurrentRighttMechanic   = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (MyMechanics[CurrentRighttMechanic] != E_MECHANICS.NONE_MECHANIC)
        {
            if (MyMechanics[CurrentRighttMechanic] == ActiveMechanics[0])
            {
                UpdateRightMechanic();
            }

            ActiveMechanics[1] = MyMechanics[CurrentRighttMechanic];
        }

        if (MyMechanics[CurrentLeftMechanic] != E_MECHANICS.NONE_MECHANIC)
        {
            if (MyMechanics[CurrentLeftMechanic] == ActiveMechanics[1])
            {
                UpdateLeftMechanic();
            }

            ActiveMechanics[0] = MyMechanics[CurrentLeftMechanic];
        }

    }

    public int GetUsefulMechanics()
    {
        int NumberMechanics = 0;

        for (int Index = 0; Index < MyMechanics.Length; Index++)
        {
            if (MyMechanics[Index] != E_MECHANICS.NONE_MECHANIC)
            {
                NumberMechanics++;
            }
        }

        return NumberMechanics;
    }

    public void RemoveMechanics()
    {
        for (int Index = 0; Index < MyMechanics.Length; Index++)
        {
            if (MyMechanics[Index] != E_MECHANICS.DASH)
            {
                MyMechanics[Index] = E_MECHANICS.NONE_MECHANIC;
            }
        }

        ActiveMechanics[0] = E_MECHANICS.DASH;
        ActiveMechanics[1] = E_MECHANICS.NONE_MECHANIC;
    }

    public E_MECHANICS[] GetMyMechanics()
    {
        return ActiveMechanics;
    }

    public void MechanicUsed(E_MECHANICS aMechanic)
    {
        NumberUsedMechanics[aMechanic]++;
    }

    public int GetMechanicUsedTimes(E_MECHANICS aMechanic)
    {
        return NumberUsedMechanics[aMechanic];
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
    
    public E_MECHANICS GetMoreUsedMechanic()
    {
        List<KeyValuePair<E_MECHANICS, int>> MyList = NumberUsedMechanics.ToList();
        MyList.Sort((x, y) => y.Value.CompareTo(x.Value));
        return MyList[0].Key;
    }

    public int GetIntMoreUsedMechanic()
    {
        List<KeyValuePair<E_MECHANICS, int>> MyList = NumberUsedMechanics.ToList();
        MyList.Sort((x, y) => y.Value.CompareTo(x.Value));
        return MyList[0].Value;
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
