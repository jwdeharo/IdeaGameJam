using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextAppearanceManager : MonoBehaviour {

    public float speed;

    int index;
    bool lineIsFinished,hold;
    WaitForSeconds waitingTime;
    Text text;
    public string sentence;
    float waitTime;
    
    //private DialogueNavigation dialogueScript; 

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
            //Wait();
            
        }

    }
   
    /*void Wait()
    {
        if (Time.time >= waitTime)
        {
            ResetTextAppeareance();
            if (dialogueScript != null)
            {
                dialogueScript.FinishLine = true;
            }
            this.enabled = false;
        }
    }*/

    IEnumerator TextDisplayer(Text text, string sentence)
    {
        text.text += sentence[index];
        index++;
        hold = true;
        yield return waitingTime;
        waitTime = Time.time + 1;
        hold = false;
    }

    public void ResetTextAppeareance()
    {
        if(text!=null)
            text.text = "";
        index = 0;
    }

    public int Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

    public Text Text
    {
        get
        {
            return text;
        }

        set
        {
            text = value;
        }
    }

    /*public char[] Sentence
    {
        get
        {
            return sentence;
        }

        set
        {
            sentence = value;
        }
    }*/

    public bool LineIsFinished
    {
        get
        {
            return lineIsFinished;
        }

        set
        {
            lineIsFinished = value;
        }
    }

   /* public DialogueNavigation DialogueScript
    {
        get
        {
            return dialogueScript;
        }

        set
        {
            dialogueScript = value;
        }
    }*/
}
