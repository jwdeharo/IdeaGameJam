using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meta : MonoBehaviour {

    public static int vueltaActual;

    public bool definitiva;
    private bool leToca;

    public Meta siguiente;
        
	void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Yikes");
		if(leToca)
        {
            leToca = false;
            siguiente.leToca = true;
            if (definitiva)
                vueltaActual++;
            Debug.Log("Vuelta actual: "+vueltaActual);
        }
	}
	
	void Start () {
        if (!definitiva)
            GetComponent<SpriteRenderer>().enabled = false;
        if (definitiva)
            leToca = true;
	}
}
