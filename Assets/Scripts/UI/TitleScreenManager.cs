using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour {

    public FadeInController fadeIn;

	void Update () {
        PressKey();

    }

    void PressKey()
    {
        if (Input.anyKeyDown)
        {
            fadeIn.LoadScene = "NewScene";
            fadeIn.End = true;
            fadeIn.gameObject.SetActive(true);
        }
    }
}
