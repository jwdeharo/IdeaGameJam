using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{

    private IState CurrentState;
    private readonly Dictionary<IState, List<CCondition>> Conditions;
    private readonly Dictionary<string, IState> States;
    private readonly string InfoPath;

    // Use this for initialization
    private void Start()
    {
        //Here we will read our json for the information of the states.
        string test = "";
        IState state_test = null;
        bool value = false;
        //first we will need to init all the states.
        //a for will be needed here. And inside that for we will create all the conditions.
        Conditions[CurrentState].Add(new CCondition(test, state_test, value, value));
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
}
