using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            DisplayNextText(tag);
        }
    }

    public void StartText(Dialogue d, string tag)
    {
        a1.SetBool("isOpen", true);
        Debug.Log("START DIALOGUE CALISS");
        Name.text = d.name;
        sentences.Clear();

        foreach (string sentence in d.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextText(tag);
        
    }

       
    public void EndText(string tag)
    {
        a1.SetBool("isOpen", false);
        if (!(SceneManager.GetSceneByName("BlacksmithShop_3-0").isLoaded))
        {
            Debug.Log(SceneManager.GetSceneByName("BlacksmithShop_3-0").isLoaded);
            SceneManager.LoadScene("BlacksmithShop_3-0", LoadSceneMode.Additive);
        }
        
    }
    public void DisplayNextText(string tag)
    {
        Debug.Log("re");
         if(sentences.Count == 0)
        {
            EndText(tag);
            return;
        }
        string s = sentences.Dequeue();
        Dialogue.text = s;
    }

}
