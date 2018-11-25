using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyManager : MonoBehaviour {

	void Awake()
    {
        if (DataManager.CanBeDestroyed)
        {
            Destroy(transform.gameObject);
            DataManager.CanBeDestroyed = false;
        }
        DataManager.CanBeDestroyed = true;
    }

}
