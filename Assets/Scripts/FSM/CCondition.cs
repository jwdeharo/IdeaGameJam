using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCondition 
{
    //Name of the condition.
    private readonly string Name;
    private readonly IState ToState;
    private readonly bool NeededValue;
    private bool CurrentValue;

    public CCondition(string aName, IState aToState, bool aValue, bool aCurrentValue)
    {
        Name = aName;
        ToState = aToState;
        NeededValue = aValue;
        CurrentValue = aCurrentValue;
    }

    public bool CheckCondition()
    {
        return NeededValue == CurrentValue;
    }

    public string GetName()
    {
        return Name;
    }

    public void SetValue(bool aValue)
    {
        CurrentValue = aValue;
    }

    public IState GetToState()
    {
        return ToState;
    }
}
