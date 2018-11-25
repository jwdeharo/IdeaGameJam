using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LapShower : MonoBehaviour {

    public int numeroDeVueltas = 3;

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = Meta.vueltaActual + "/" + numeroDeVueltas;

        if (Meta.vueltaActual > 0)
            Victory();
	}

    private void Victory()
    {
        string sceneName = PlayerPrefs.GetString("lastLoadedScene");
        SceneManager.LoadScene(sceneName);//back to previous scene1?
    }
}
