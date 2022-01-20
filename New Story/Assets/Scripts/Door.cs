using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Item[] itemIfNeed;
    [SerializeField] string levelIndex;
    [SerializeField] string doorName;
    [SerializeField] Inventory inventory;
    private LevelLoader loader;
    private DialogueWarning dialogue;

    private void Awake()
    {
        loader = FindObjectOfType<LevelLoader>();
        dialogue = GetComponent<DialogueWarning>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.statusText.text = doorName;
    }

    public void Action()
    {
        bool ok = true;
        foreach (Item key in itemIfNeed)
        {
            ok = false;
            foreach (Item item in inventory.GetItemList())
            {
                if (key.itemType == item.itemType)
                {
                    ok = true;
                    break;
                }
            }
            if (!ok)
            {
                if(dialogue != null)
                    dialogue.TriggerDialogue();
                return;
            }
        }
        if (ok)
            loader.LoadNextLevel(levelIndex);
        return;
    }
}

