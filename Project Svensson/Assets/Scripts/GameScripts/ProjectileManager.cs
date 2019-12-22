using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileManager : MonoBehaviour
{
    public TextMeshProUGUI ammoBox;
   // public TextMeshProUGUI ammoBoxSecondary;
    public Inventory playerInventory;
    public Item phone;
    public Item rubber;
    //public Item something;
    // Start is called before the first frame update
    void Start()
    {
        if (playerInventory.CheckForItem(phone))
        {

            ammoBox.text = "" + playerInventory.phone;
        }
        if (playerInventory.CheckForItem(rubber))
        {

            ammoBox.text = "" + playerInventory.rubber;
        }


    }
    void Update()
    {
        if (playerInventory.CheckForItem(phone))
        {

            ammoBox.text = "" + playerInventory.phone;
        }
        if (playerInventory.CheckForItem(rubber))
        {

            ammoBox.text = "" + playerInventory.rubber;
        }
    }

    // Update is called once per frame
    public void AddAmmo()
    {
        if (playerInventory.CheckForItem(phone))
        {

            playerInventory.phone += 1;
            ammoBox.text = "" + playerInventory.phone;
        }
        if (playerInventory.CheckForItem(rubber))
        {
            playerInventory.rubber += 1;
            ammoBox.text = "" + playerInventory.rubber;
        }
        }

    public void DecreaseAmmo()
    {
        if (playerInventory.CheckForItem(phone))
        {
            playerInventory.phone -= 1;
            ammoBox.text = "" + playerInventory.phone;

        }
        if (playerInventory.CheckForItem(rubber))
        {
            playerInventory.rubber -= 1;
            ammoBox.text = "" + playerInventory.rubber;

        }


    }

}
