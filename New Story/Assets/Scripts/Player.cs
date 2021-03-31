using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private UIInventory uiInventory;
    [SerializeField] Inventory inventory;
    public DialogueManager dialogueManager;
    public LevelLoader loader;
    public static TextMeshPro statusText;

    private SpriteRenderer graphics;
    private Animator animator;
    private Rigidbody2D playerRigidbody2D;

    private const float MOVE_SPEED = 6f;
    private Vector3 move;
    private bool isStop = false;

    private void Awake()
    {
        statusText = transform.GetChild(0).GetComponent<TextMeshPro>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        uiInventory.SetPlayer(this);
        graphics = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ItemWorld itemWorld = collision.GetComponent<ItemWorld>();

                // touching item
                inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        statusText.text = "";
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
