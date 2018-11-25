using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreifExistence : MonoBehaviour {

    public float timeRemaining;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0)
            GameOver();
	}

    private void GameOver()
    {
        //Llamar al gameOverGeneral
        Debug.Log("Game Over");
    }
}
