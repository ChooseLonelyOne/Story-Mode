using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWarning : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerDialogue()
    {
        OnOneTime();
    }

    private void OnOneTime()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
