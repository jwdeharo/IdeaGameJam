using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 10;
    public Vector2 direction;
    public GameObject Source;
    public string Destination;
	
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed / 100) ;
	}

    private void OnTriggerEnter(Collider col)
    {
        string ColTag = col.gameObject.transform.parent.gameObject.tag;
        if (ColTag .CompareTo(Destination) ==0)
        {
            Affect(col);
        }
        Destroy(gameObject);
    }

    protected virtual void Affect(Collider col)
    {
        if (col.gameObject.transform.parent.gameObject.name != "Player")
        {
            Destroy(col.gameObject.transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.CompareTo(Source.name) != 0)
        {
            Destroy(gameObject);
        }
    }
}
