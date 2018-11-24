using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{

    //Dictionary that relates each state with its conditions.
    private Dictionary<IState, List<CCondition>> Conditions;

    //List of all the states that this FSM has.
    private Dictionary<IState, string> States;

    //This stack will allow us to have memory of the last state.
    private List<IState> StackOfStates;

    // Use this for initialization
    private void Awake()
    {
        //In a future it would be cool to have a File Manager that will read
        //from a json.
        Conditions = new Dictionary<IState, List<CCondition>>();
        States = new Dictionary<IState, string>();
        StackOfStates = new List<IState>();
        Debug.Log(States);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //We always do the update of the state at the top.
        if (StackOfStates.Count != 0)
        {
            StackOfStates[0].UpdateState();
        }
    }

    public void CheckConditions(CCondition aCondition)
    {
        if (aCondition != null && aCondition.CheckCondition())
        {
            StackOfStates[0].OnExitState();

            //If the stack on the top is not the same as the condition says, we insert.
            //If it is the same, we do not insert because we already have it on the stack.
            if (StackOfStates[0] != aCondition.GetToState())
            {
                StackOfStates.Insert(0, aCondition.GetToState());
            }

            StackOfStates[0].OnEnterState();
        }
    }

    public void PopState()
    {
        StackOfStates.RemoveAt(0);
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

    public void SetFSMCondition(string aName, bool aConditionValue)
    {
        //When we have to set a new value, we set it for all conditions in all states because it is shared.

        CCondition ToCheck = null;
        foreach (KeyValuePair<IState, List<CCondition>> ConditionValuePair in Conditions)
        {
            List<CCondition> ConditionList = ConditionValuePair.Value;
            IState ConditionState = ConditionValuePair.Key;
            foreach (CCondition Condition in ConditionList)
            {
                if (Condition.GetName().Equals(aName) && Condition.GetValue() != aConditionValue)
                {

                    Condition.SetValue(aConditionValue);
                    if (ConditionState == StackOfStates[0])
                    {
                        ToCheck = Condition;
                    }
                    //We check the conditions every time we detect some one has been changed.
                    break;
                }
            }
        }

        CheckConditions(ToCheck);
    }

    public void AddState(string aName, IState aState)
    {
        if (!States.ContainsKey(aState))
        {
            States[aState] = aName;
        }

        if (StackOfStates.Count == 0)
        {
            StackOfStates.Insert(0, aState);
            StackOfStates[0].OnEnterState();
        }
    }

    public void AddCondition(IState aState, CCondition aCondition)
    {
        if (!Conditions.ContainsKey(aState))
        {
            Conditions.Add(aState, new List<CCondition>());
        }

        Conditions[aState].Add(aCondition);
    }

    public string GetCurrentState()
    {
        string CurrentStateStr = "";

        if (States.ContainsKey(StackOfStates[0]))
        {
            CurrentStateStr = States[StackOfStates[0]];
        }

        return CurrentStateStr;
    }
}
