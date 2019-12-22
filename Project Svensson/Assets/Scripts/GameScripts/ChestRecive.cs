using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestRecive : Interactable
{ 
    public Dialog dialog;
    public Item content;
    public Inventory inventory;
    public bool isOpen;
    public BoolValue open;
    public Signal_Event raiseItem;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       anim = GetComponent<Animator>();
        isOpen = open.RuntimeValue;
        if (isOpen == true)
        {
           anim.SetBool("opened", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && playerInRange)
        {
            if (!isOpen)
            {
                //open chest
                OpenChest();
            }
            else
            {
                //chest is opened
                ChestAlreadyOpen();
            }
        }
    }
    public void OpenChest()
    {
        //Dialog window on
        FindObjectOfType<DialogManager>().StartDialog(dialog);
        //dialogText.text = content.itemDescription;
        inventory.AddItem(content);
        inventory.currentItem = content;
        raiseItem.Raise();
        context.Raise();
        isOpen = true;
        anim.SetBool("opened", true);
        open.RuntimeValue = isOpen;
    }
    public void ChestAlreadyOpen()
    {


        raiseItem.Raise();


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && ! isOpen)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}
