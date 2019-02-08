using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text Name;
    public Text Dialogue;
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartText(Dialogue d)
    {
        Debug.Log("START DIALOGUE CALISS");
        Name.text = d.name;
        sentences.Clear();

        foreach (string sentence in d.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextText();
    }

       
    public void EndText()
    {

    }
    public void DisplayNextText()
    {
         if(sentences.Count == 0)
        {
            EndText();
            return;
        }
        string s = sentences.Dequeue();
        Dialogue.text = s;
    }

}
