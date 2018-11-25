using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FadeInController : MonoBehaviour {

    private Image image;
    private bool end;
    private string loadScene;

    public GameObject loading;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        Fade();
    }

    void Fade()
    {
        Color alpha = image.color;
        alpha.a += 0.01f;
        image.color = alpha;
        if (alpha.a >= 1)
        {
            alpha.a = 1;
            image.color = alpha;
            if(!end)
                this.enabled = false;
            if (end)
            {
                if(loading!=null)
                    loading.SetActive(true);
                SceneManager.LoadScene(loadScene, LoadSceneMode.Single);
            }
        }
    }

    public bool End
    {
        get
        {
            return end;
        }

        set
        {
            end = value;
        }
    }

    public string LoadScene
    {
        get
        {
            return loadScene;
        }

        set
        {
            loadScene = value;
        }
    }
}
