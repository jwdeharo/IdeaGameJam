using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeOutController : MonoBehaviour {

    public bool flash,deactivate;
    public float fadeSpeed;

    private Text text;
    private Image image;
    
    void Awake () {
        fadeSpeed = 0.01f;
        if (flash)
            text = GetComponent<Text>();
        else
            image = GetComponent<Image>();
	}

    void Update () {
        if (flash)
            Fade();
        else
            FadeImage();
    }

    public void Fade()
    {
        Color alpha = text.color;
        alpha.a-= fadeSpeed;
        text.color = alpha;

        if (alpha.a <= 0)
        {
            alpha.a = 1;
            text.color = alpha;
            transform.gameObject.SetActive(false);
        }
    }

    public void FadeImage()
    {
        Color alpha = image.color;
        alpha.a -= fadeSpeed;
        image.color = alpha;

        if (alpha.a <= 0)
        {
            if (!deactivate)
            {
                alpha.a = 0;
                this.enabled = false;
            }
            else
            {
                alpha.a = 1;
                transform.gameObject.SetActive(false);
            }
            image.color = alpha;
        }
    }
}
