using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] string levelindex;
    public Item item;
    public List<Dialogue> dialogue;
    private bool show = false;
    private GameObject col;
    private bool trigger;

    private void Update()
    {
        if(trigger)
            if (Input.GetKeyDown(KeyCode.E))
            {
                Action(col);
                return;
            }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        trigger = true;
        col = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
        col = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        if (GetDialogueTrigger())
            TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        foreach (Dialogue dial in dialogue)
        {
            if (!dial.repeat)
            {
                if (!show)
                    OnOneTime(ref show, dial);
                return;
            }
            if (dial.repeat)
                OnSomeTime(dial);
        }
    }

    private void OnOneTime(ref bool show, Dialogue dial)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dial);
        dialogue.Remove(dial);
        show = true;
    }

    private void OnSomeTime(Dialogue dial)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dial);
    }

    private void Action(GameObject collision)
    {
        foreach (Item item1 in inventory.GetItemList())
        {
            if (item != null && item1.itemType == item.itemType && gameObject.tag == "Door")
            {
                inventory.RemoveItem(item1);
                collision.GetComponent<Player>().loader.LoadNextLevel(levelindex);
                return;
            }
            else
                TriggerDialogue();
            return;
        }
        TriggerDialogue();
        return;
    }

    public bool GetDialogueTrigger()
    {
        foreach (Dialogue dial in dialogue)
        {
            if (dial.trigger)
                return dial.trigger;
        }
        return false;
    }
}
