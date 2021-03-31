using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public List<Dialogue> dialogue;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.E))
            TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        foreach (Dialogue dial in dialogue)
        {
            OnSomeTime(dial);
            return;
        }
    }

    private void OnSomeTime(Dialogue dial)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dial);
    }
}
