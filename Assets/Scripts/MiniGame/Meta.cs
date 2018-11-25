using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meta : MonoBehaviour {

    public static int metasCompletadas;

    public bool primera;
    private bool leToca;

    public Meta siguiente;
        
	void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Yikes");
		if(leToca)
        {
            leToca = false;
            GetComponent<SpriteRenderer>().enabled = false;
            metasCompletadas++;
            if (siguiente != null)
            {
                siguiente.leToca = true;
                siguiente.GetComponent<SpriteRenderer>().enabled = true;
            }
            Debug.Log("Vuelta actual: "+metasCompletadas);
        }
	}
	
	void Start () {
        if (primera)
            leToca = true;
        else
            GetComponent<SpriteRenderer>().enabled = false;
    }
}
