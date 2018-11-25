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


        NUM_MECHANICS
    }

    private const int ActivableMechanics = 2;

    private E_MECHANICS[] MyMechanics;
    private E_MECHANICS[] ActiveMechanics;
    public int CurrentLeftMechanic;
    public int CurrentRighttMechanic;
    private Dictionary<E_MECHANICS, int> NumberUsedMechanics;
    public List<E_MECHANICS> UnlockedMechanics;


    // Use this for initialization
    void Start ()
    {
        MyMechanics             = new E_MECHANICS[(int)E_MECHANICS.NUM_MECHANICS];
        NumberUsedMechanics     = new Dictionary<E_MECHANICS, int>();


        for (int i = 0; i < (int)E_MECHANICS.NUM_MECHANICS; ++i)
        {
            MyMechanics[i] = (E_MECHANICS)i;
            NumberUsedMechanics.Add((E_MECHANICS)i, 0);
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
}

    