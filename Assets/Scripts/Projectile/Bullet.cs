using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.right * speed / 100) ;
	}

    private void OnTriggerEnter(Collider col)
    {
        string ColTag = col.gameObject.transform.parent.gameObject.tag;
        Debug.Log("Just crashed with: " + ColTag);
        if (ColTag .CompareTo( "Enemy")==0)
        {
            Destroy(col.gameObject.transform.parent.gameObject);
        }
        Destroy(gameObject);
    }
}
