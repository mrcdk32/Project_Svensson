using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{

    public Item currentItem;
    [Header("Primary")]
    public Item[] primaryWeapon=new Item[1];
    [Header("Secondary")]
    public Item[] secondaryWeapon = new Item[2];
    public Item[] tempWeapon = new Item[1];
    [Header("KeyAmount")]
    public int yellowKey;
    public int blueKey;
    public int redKey;
    public int greenKey;
    [Header("Amount")]
    public float phone;
    public float rubber;
    public int coins;
    public float maxStamina = 100;
    public float currentStamina;

    public void OnEnable()
    {
       currentStamina = maxStamina;
    }

     public void ReduceMagic(float staminaCost)
     {
         currentStamina -= staminaCost;
     }


 

    /*IEnumerator ReplaceItem(Item itemReplace)
    {
        if(((IList)secondaryWeapon).Contains(itemReplace))
        {
            secondaryWeapon[1] = itemReplace;

        }
        if (((IList)primaryWeapon).Contains(itemReplace))
        {
            primaryWeapon[1] = itemReplace;

        }
        else
            return false;

    }
    */


    public bool CheckForItem(Item item)
    {
        if (((IList)secondaryWeapon).Contains(item) && secondaryWeapon[0]==item)
        {
            return true;
        }
        if(((IList)primaryWeapon).Contains(item))
        {
            return true;
        }
        return false;
    }

    public void Switch()
    {

        
            if(secondaryWeapon[0] !=null && secondaryWeapon[1] != null)
            {
                System.Array.Reverse(secondaryWeapon);
            }
        
    }



    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd.isYellowKey)
        {
            yellowKey++;
        }
        if (itemToAdd.isBlueKey)
        {
            blueKey++;
        }
        if (itemToAdd.isRedKey)
        {
            redKey++;
        }
        if (itemToAdd.isGreenKey)
        {
            greenKey++;
        }

        else if(!((IList)primaryWeapon).Contains(itemToAdd) && itemToAdd.primary)
        {
            if(!((IList)primaryWeapon).Contains(itemToAdd) && itemToAdd.primary)
            {
                primaryWeapon[0] = itemToAdd;
            }
            else
            {
                return;
            }
        }
        else
        {
            if (!((IList)secondaryWeapon).Contains(itemToAdd) && secondaryWeapon[0]==null && itemToAdd.secondary)
            {
                secondaryWeapon[0] = itemToAdd;
              
               /* else
                {
                    return;
                }*/
            }
            else if (!((IList)secondaryWeapon).Contains(itemToAdd && itemToAdd.secondary))
            {
                    secondaryWeapon[1] = itemToAdd;

            }
            else
            {
                return;
            }
            

        }
        //StartCoroutine(ReplaceItem(itemReplace));
    }
}
