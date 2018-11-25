using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextAppearanceManager : MonoBehaviour {

    public float speed;
    public string sentence;
    public GameObject title;

    int index;
    bool lineIsFinished,hold;
    WaitForSeconds waitingTime;
    Text text;
    float waitTime;
    
    private void Start()
    {
        text = GetComponent<Text>();
        waitingTime = new WaitForSeconds(speed);
        index = 0;
    }

    private void Update()
    {

        if (index < sentence.Length)
        {
            lineIsFinished = false;
            if (!hold)
                StartCoroutine(TextDisplayer(text, sentence));
        }
        else
        {
            lineIsFinished = true;
            PressKey();
       }

    }
   
    void PressKey()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
            title.SetActive(true);
        }
    }

    IEnumerator TextDisplayer(Text text, string sentence)
    {
        text.text += sentence[index];
        index++;
        hold = true;
        yield return waitingTime;
        waitTime = Time.time + 1;
        hold = false;
    }

    
}
