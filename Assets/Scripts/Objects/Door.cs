using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public BoolValue doorOpened;

    private void Start()
    {   
        if(thisDoorType == DoorType.key)
        {
            if (doorOpened.RuntimeValue)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("attack"))
        {
            if(playerInRange && thisDoorType == DoorType.key)
            {
                if(playerInventory.numberOfKeys > 0)
                {
                    playerInventory.numberOfKeys--;
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        doorSprite.enabled = false;
        open = true;
        if (thisDoorType == DoorType.key)
        {
            doorOpened.RuntimeValue = true;
        }
        physicsCollider.enabled = false;
    }

    public void Close()
    {
        doorSprite.enabled = true;
        open = false;
        physicsCollider.enabled = true;
    }
}
