using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
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

        NONE_MECHANIC,

        NUM_MECHANICS
    }

    private const int ActivableMechanics = 2;

    private static E_MECHANICS[] ActiveMechanics;
    public int CurrentLeftMechanic;
    public int CurrentRighttMechanic;
    private Dictionary<E_MECHANICS, int> NumberUsedMechanics;
    public List<E_MECHANICS> UnlockedMechanics;


    // Use this for initialization
    void Start ()
    {
        NumberUsedMechanics     = new Dictionary<E_MECHANICS, int>();
        UnlockedMechanics       = new List<E_MECHANICS>();
        
        for (int i = 0; i < (int)E_MECHANICS.NUM_MECHANICS; ++i)
        {
            NumberUsedMechanics.Add((E_MECHANICS)i, 0);
            UnlockedMechanics.Add(E_MECHANICS.NONE_MECHANIC);
        }

        ActiveMechanics         = new E_MECHANICS[2];
        ActiveMechanics[0]      = E_MECHANICS.DASH;
        ActiveMechanics[1]      = E_MECHANICS.NONE_MECHANIC;

        UnlockedMechanics[(int)E_MECHANICS.DASH] = E_MECHANICS.DASH;

        CurrentLeftMechanic     = 0;
        CurrentRighttMechanic   = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GetUsefulMechanics() > 1)
        {
            if (UnlockedMechanics[CurrentRighttMechanic] == ActiveMechanics[0])
            {
                UpdateRightMechanic();
                ActiveMechanics[1] = UnlockedMechanics[CurrentRighttMechanic];
            }

            if (UnlockedMechanics[CurrentLeftMechanic] == ActiveMechanics[1])
            {
                UpdateLeftMechanic();
                ActiveMechanics[0] = UnlockedMechanics[CurrentLeftMechanic];
            }
        }

    }

    public int GetUsefulMechanics()
    {
        int NumberMechanics = 0;

        for (int Index = 0; Index < UnlockedMechanics.Count; Index++)
        {
            if (UnlockedMechanics[Index] != E_MECHANICS.NONE_MECHANIC)
            {
                NumberMechanics++;
            }
        }

        return NumberMechanics;
    }

    public void RemoveMechanics()
    {
        for (int Index = 0; Index < UnlockedMechanics.Count; Index++)
        {
            if (UnlockedMechanics[Index] != E_MECHANICS.DASH)
            {
                UnlockedMechanics[Index] = E_MECHANICS.NONE_MECHANIC;
            }
        }

        ActiveMechanics[0] = E_MECHANICS.DASH;
        ActiveMechanics[1] = E_MECHANICS.NONE_MECHANIC;
    }

    public E_MECHANICS[] GetMyMechanics()
    {
        return ActiveMechanics;
    }

    public static E_MECHANICS GetCurrentMechanic(int aIndex)
    {
        return ActiveMechanics[aIndex];
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
        if (CurrentLeftMechanic < UnlockedMechanics.Count - 1)
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
        if (CurrentRighttMechanic < UnlockedMechanics.Count - 1)
        {
            CurrentRighttMechanic++;
        }
        else
        {
            CurrentRighttMechanic = 0;
        }
    }

    public void UnlockMechanic(E_MECHANICS aMechanic)
    {
        UnlockedMechanics[(int)aMechanic] = aMechanic;

        if (ActiveMechanics[1] == E_MECHANICS.NONE_MECHANIC)
        {
            Debug.Log(aMechanic);
            ActiveMechanics[1] = aMechanic;
        }
    }
}

    