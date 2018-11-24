using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{

    //Current state. In a future we might want to have a stack of it.
    private IState CurrentState;
    //Dictionary that relates each state with its conditions.
    private Dictionary<IState, List<CCondition>> Conditions;
    //List of all the states that this FSM has.
    private Dictionary<string, IState> States;

    // Use this for initialization
    private void Start()
    {
        //In a future it would be cool to have a File Manager that will read
        //from a json.
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CurrentState.UpdateState();
    }

    private void LateUpdate()
    {
        //We check conditions here.
        List<CCondition> ConditionList = GetConditionList(CurrentState);
        foreach (CCondition Condition in ConditionList)
        {
            if (Condition.CheckCondition())
            {
                CurrentState.OnExitState();
                CurrentState = Condition.GetToState();
                CurrentState.OnEnterState();
                break;
            }
        }
    }

    private List<CCondition> GetConditionList(IState aState)
    {
        List<CCondition> ReturnListCondition = new List<CCondition>();

        if (Conditions.ContainsKey(aState))
        {
            ReturnListCondition = Conditions[aState];
        }

        return ReturnListCondition;
    }

    private void SetFSMCondition(string aName, bool aConditionValue)
    {
        List<CCondition> ConditionList = GetConditionList(CurrentState);

        foreach (CCondition Condition in ConditionList)
        {
            if (Condition.GetName().Equals(aName))
            {
                Condition.SetValue(aConditionValue);
                break;
            }
        }
    }

    public void AddCondition(IState aState, CCondition aCondition)
    {
        Conditions[aState].Add(aCondition);
    }
}
