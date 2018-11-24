using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRenderer : MonoBehaviour {

    private Text text;

    public BreifExistence timer;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = timer.timeRemaining.ToString("0.00"); ;
    }
}
