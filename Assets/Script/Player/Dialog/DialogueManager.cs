using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text Name;
    public Text Dialogue;
    public Button continueButton;
    private Queue<string> sentences;

    public Animator a1;

    // Start is called before the first frame update
    void Start()
    {
        continueButton.enabled = true;
        continueButton.interactable = true;
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            DisplayNextText();
        }
    }

    public void StartText(Dialogue d)
    {
        a1.SetBool("isOpen", true);
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
        a1.SetBool("isOpen", false);
    }
    public void DisplayNextText()
    {
        Debug.Log("re");
         if(sentences.Count == 0)
        {
            EndText();
            return;
        }
        string s = sentences.Dequeue();
        Dialogue.text = s;
    }

}
