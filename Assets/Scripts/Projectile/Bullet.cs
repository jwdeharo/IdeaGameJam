using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 10;

    public Vector2 direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed / 100) ;
	}

    private void OnTriggerEnter(Collider col)
    {
        string ColTag = col.gameObject.transform.parent.gameObject.tag;
        Debug.Log("Just crashed with: " + ColTag);
        if (ColTag .CompareTo( "Enemy")==0)
        {
            Affect(col);
        }
        Destroy(gameObject);
    }

    protected virtual void Affect(Collider col)
    {
        Destroy(col.gameObject.transform.parent.gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision with!: "+col.gameObject.name);
        if (col.gameObject.name.CompareTo("Player") != 0)
            Destroy(gameObject);
    }
}
