using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LapShower : MonoBehaviour {

    public int HabilidadDesbloqueada;
    public int numeroDeVueltas = 3;

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        numeroDeVueltas = GameObject.FindObjectsOfType<Meta>().Length;
	}
	
	// Update is called once per frame
	void Update () {
        text.text = Meta.metasCompletadas + "/" + numeroDeVueltas;

        if (Meta.metasCompletadas == numeroDeVueltas)
            Victory();
	}

    private void Victory()
    {
        string sceneName = PlayerPrefs.GetString("lastLoadedScene");
        PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneName);//back to previous scene1?
    }
}
