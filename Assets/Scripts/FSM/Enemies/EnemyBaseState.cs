using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    protected string MyName;
    protected GameObject MyGameObject;

    public GameObject GetMyGameObject(string aName)
    {
        GameObject MyObject = GameObject.Find(aName);

        return MyObject;
    }

    public void SetMyName(string aMyName)
    {
        MyName = aMyName;
    }

    public void SetMyGameObject(GameObject aGameObject)
    {
        MyGameObject = aGameObject;
    }
}
