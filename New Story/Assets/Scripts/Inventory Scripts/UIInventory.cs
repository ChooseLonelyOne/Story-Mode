using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using CodeMonkey.Utils;

public class UIInventory : MonoBehaviour
{
    private Animator animator;
    [SerializeField] Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Player player;
    private bool isOpen = false;
    private Color unSelected = new Color(0, 0, 0, 0.8f);
    private Color selected = new Color(1, 1, 1, 0.8f);
    private int x;
    private TextMeshPro statusTextUI;

    public bool IsOpen
    {
        get
        {
            return isOpen;
        }
        set
        {
            isOpen = value;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    private void Start()
    {
        statusTextUI = player.gameObject.transform.GetChild(2).GetComponent<TextMeshPro>();
        SetInventory();
    }
    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void SetInventory()
    {
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItem();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            AnimateInventory();

        if (isOpen)
        {
            if (inventory.GetItemList().Count != 0)
            {
                NavigationInInventory();
                statusTextUI.text = inventory.GetItemList()[x - 1].itemType.ToString();
            }
        }
        else
        {
            statusTextUI.text = "";
        }
    }

    private void NavigationInInventory()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (x < inventory.GetItemList().Count)
            {
                itemSlotContainer.GetChild(x).GetComponent<Image>().color = unSelected;
                x++;
                itemSlotContainer.GetChild(x).GetComponent<Image>().color = selected;
                Debug.Log(x);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (x > 1)
            {
                itemSlotContainer.GetChild(x).GetComponent<Image>().color = unSelected;
                x--;
                itemSlotContainer.GetChild(x).GetComponent<Image>().color = selected;
                Debug.Log(x);
            }
        }
    } 

    public void AnimateInventory()
    {
        if (!isOpen)
        {
            isOpen = true;
            x = 1;
            Selected();
            animator.SetBool("isOpen", true);
            player.StopPlayer();
            return;
        }
        if (isOpen)
        {
            isOpen = false;
            Diselected();
            animator.SetBool("isOpen", false);
            player.StopPlayer();
            statusTextUI.SetText("");
            x = 1;
            return;
        }
    }

    private void Diselected()
    {
        int y = 1;
        foreach (Item item in inventory.GetItemList())
        {
            itemSlotContainer.GetChild(y).GetComponent<Image>().color = unSelected;
            y++;
        }
    }

    private void Selected()
    {
        if (itemSlotContainer.childCount > 1)
        {
            itemSlotContainer.GetChild(x).GetComponent<Image>().color = selected;
        }
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItem();
    }

    private void RefreshInventoryItem()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate)
                continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 170f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetUISprite();
            image.SetNativeSize();

            x++;
            if (x > 8)
            {
                x = 0;
                y++;
            }
        }
    }

    private void OnDestroy()
    {
        inventory.OnItemListChanged -= Inventory_OnItemListChanged;
    }
}
