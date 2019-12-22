using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyManager : MonoBehaviour
{
    public bool isYellowKey = false;
    public bool isBlueKey = false;
    public bool isRedKey = false;
    public bool isGreenKey = false;
    public TextMeshProUGUI keyBox;
    public Inventory playerInventory;

    public void KeyAmount()
    {
        if (isBlueKey)
        {
            keyBox.text = "" + playerInventory.blueKey;
        }
        if(isYellowKey)
        {
            keyBox.text = "" + playerInventory.yellowKey;

        }
        if (isGreenKey)
        {
            keyBox.text = "" + playerInventory.greenKey;

        }
        if (isRedKey)
        {
            keyBox.text = "" + playerInventory.redKey;

        }
    }
 
}
