using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private UIInventory uiInventory;
    [SerializeField] Inventory inventory;
    public DialogueManager dialogueManager;
    public LevelLoader loader;
    public static TextMeshPro statusText;
    public static Vector2 lastPosition;

    private SpriteRenderer graphics;
    private Animator animator;
    private Rigidbody2D playerRigidbody2D;

    private const float MOVE_SPEED = 6f;
    private Vector3 move;
    private bool isStop = false;
    private bool isInteractive = false;
    private GameObject colObject;

    private void Awake()
    {
        transform.position = lastPosition;
        lastPosition = transform.position;
        statusText = transform.GetChild(0).GetComponent<TextMeshPro>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        uiInventory.SetPlayer(this);
        graphics = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(isInteractive && Input.GetKeyDown(KeyCode.E))
            InteractWithColliders();

        float moveX = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            moveX = Input.GetAxisRaw("Horizontal");
        move = new Vector2(moveX, 0);

        if (moveX < 0 && !isStop)
        {
            graphics.flipX = true;
            animator.SetBool("isMoving", true);
        }
        else if (moveX > 0 && !isStop)
        {
            graphics.flipX = false;
            animator.SetBool("isMoving", true);
        }
        else
            animator.SetBool("isMoving", false);
    }

    private void FixedUpdate()
    {
        if (!isStop)
            playerRigidbody2D.velocity = move * MOVE_SPEED;
        else
            playerRigidbody2D.velocity = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInteractive = true;
        colObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInteractive = false;
        colObject = null;
        statusText.text = "";
    }
    private void InteractWithColliders()
    {
        DialogueTrigger trigger = colObject.GetComponent<DialogueTrigger>();
        if (colObject.tag == "Item")
        {
            inventory.AddItem(colObject.GetComponent<ItemWorld>().GetItem());
            Destroy(colObject);
            if(trigger != null)
                trigger.TriggerDialogue();
        }
        if(colObject.tag == "Door")
        {
            colObject.GetComponent<Door>().Action();
        }
    }

    public Vector3 GetPosition()
    {
        return new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void StopPlayer()
    {
        if (dialogueManager.IsDialogue || uiInventory.IsOpen)
        {
            statusText.gameObject.SetActive(false);
            isStop = true;
        }
        else
        {
            statusText.gameObject.SetActive(true);
            isStop = false;
        }
    }
}
