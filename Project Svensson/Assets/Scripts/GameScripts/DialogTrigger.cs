using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public bool playerInRange;
    public Signal_Event context;

    public virtual void Update()
    {
        if (Input.GetButtonDown("Attack") && playerInRange)
        {
            FindObjectOfType<DialogManager>().StartDialog(dialog);


        }
        else if (Input.GetButtonDown("Next"))
        {
            FindObjectOfType<DialogManager>().DisplayNextSentence();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            context.Raise();
            playerInRange = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            context.Raise();
            FindObjectOfType<DialogManager>().EndDialog();
            playerInRange = false;
        }
    }
}
