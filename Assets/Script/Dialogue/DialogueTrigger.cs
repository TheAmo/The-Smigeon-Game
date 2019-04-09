using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string Shopname;
    public Dialogue dialogue; 
    public GameObject shopkeeper;
    public DialogueTrigger(Dialogue d)
    {
        
       
        dialogue = d;
    }
    public void TriggerDialogue()
    {
        Debug.Log("-------------------------------------------------" + name);
        FindObjectOfType<DialogueManager>().StartText(dialogue, name);
    }
}
