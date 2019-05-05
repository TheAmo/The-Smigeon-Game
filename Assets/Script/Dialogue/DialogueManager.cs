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
    public GameObject player;
    public Animator a1;
    private string m_name;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

    public void StartText(Dialogue d, string name)
    {
        m_name = name;
        a1.SetBool("isOpen", true);
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
        if(m_name == "Blacksmith")
        {

            GameObject.FindGameObjectWithTag("Blacksmith").transform.Find("Shop").gameObject.SetActive(true);
            //GameObject.FindGameObjectWithTag("ShopB").SetActive(true);
        }
        else if(m_name == "Armorer")
        {
            GameObject.FindGameObjectWithTag("Armorer").transform.Find("Shop").gameObject.SetActive(true);

        }
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
