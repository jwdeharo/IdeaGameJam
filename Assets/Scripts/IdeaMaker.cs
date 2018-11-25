using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdeaMaker : MonoBehaviour {

    public string SceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
        Debug.Log("IdeaMaker");
    }
}
