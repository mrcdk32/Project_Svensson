using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class InventoryDisplayWeapon : MonoBehaviour
{

    public Inventory inventory;
    public Signal_Event raiseItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventory.primaryWeapon[0] == null)
        {
            raiseItem.Raise();
        }
        else
        {
            raiseItem.Raise();
        }
    }
}
