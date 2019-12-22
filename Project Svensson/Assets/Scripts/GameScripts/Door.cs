using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    redkey,
    bluekey,
    yellowkey,
    greenkey,
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
    public Signal_Event keyAmount;

    public void start()
    {
        keyAmount.Raise();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (playerInRange && thisDoorType == DoorType.redkey)
            {
                //Does the player have a key?
                if (playerInventory.redKey > 0)
                {
                    //Remove a player key
                    keyAmount.Raise();
                    playerInventory.redKey--;
                    //If so, then call the open method
                    Open();
                }
            }
        }
        if (Input.GetButtonDown("Attack"))
        {
            if (playerInRange && thisDoorType == DoorType.yellowkey)
            {
                //Does the player have a key?
                if (playerInventory.yellowKey > 0)
                {
                    keyAmount.Raise();
                    //Remove a player key
                    playerInventory.yellowKey--;
                    //If so, then call the open method
                    Open();
                }
            }
        }
        if (Input.GetButtonDown("Attack"))
        {
            if (playerInRange && thisDoorType == DoorType.greenkey)
            {
                //Does the player have a key?
                if (playerInventory.greenKey > 0)
                {
                    keyAmount.Raise();
                    //Remove a player key
                    playerInventory.greenKey--;
                    //If so, then call the open method
                    Open();
                }
            }
        }
        if (Input.GetButtonDown("Attack"))
        {
            if (playerInRange && thisDoorType == DoorType.bluekey)
            {
                //Does the player have a key?
                if (playerInventory.blueKey > 0)
                {
                    keyAmount.Raise();
                    //Remove a player key
                    playerInventory.blueKey--;
                    //If so, then call the open method
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        //Turn off the door's sprite renderer
        doorSprite.enabled = false;
        //set open to true
        open = true;
        //turn off the door's box collider
        physicsCollider.enabled = false;
    }

    public void Close()
    {
        //Turn off the door's sprite renderer
        doorSprite.enabled = true;
        //set open to true
        open = false;
        //turn off the door's box collider
        physicsCollider.enabled = true;
    }
}
