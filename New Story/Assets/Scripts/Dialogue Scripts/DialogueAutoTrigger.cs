using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAutoTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        OnOneTime();
    }

    private void OnOneTime()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        Destroy(this);
    }
}
