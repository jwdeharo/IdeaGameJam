using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWhereYouWalk : MonoBehaviour {

    private GameObject inkToDraw;

	// Use this for initialization
	void Start () {
        inkToDraw = (GameObject) Resources.Load("DrawingMaterial");
	}
	
	// Update is called once per frame
	void Update () {
        Instantiate(inkToDraw, transform.position, Quaternion.identity);
	}
}
