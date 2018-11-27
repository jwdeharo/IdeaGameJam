using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        print("player " + collision.gameObject);
        Animator anim = gameObject.GetComponentInParent<Animator>();
        anim.SetBool("open", true);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print("player " + hit.gameObject);
        Animator anim = gameObject.GetComponentInParent<Animator>();
        anim.SetBool("open", true);
    }
}
