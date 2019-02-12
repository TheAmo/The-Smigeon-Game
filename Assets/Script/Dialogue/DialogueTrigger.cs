﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue; 
    public DialogueTrigger(Dialogue d)
    {
        dialogue = d;
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartText(dialogue);
    }
}