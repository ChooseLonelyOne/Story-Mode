using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;
    private DialogueTrigger trigger;

    private void Start()
    {
        trigger = transform.GetComponent<DialogueTrigger>();
        if (trigger != null)
            ItemWorld.SpawnItemWorld(transform.position, item, trigger.dialogue);
        else
            ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);
    }
}
