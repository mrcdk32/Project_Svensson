using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public Slider staminaSlider;
    public Inventory playerInventory;
    public float decrease;
    // Start is called before the first frame update
    void Start()
    {
        staminaSlider.maxValue = playerInventory.maxStamina;
        staminaSlider.value = playerInventory.maxStamina;
        playerInventory.currentStamina = playerInventory.maxStamina;
    }

    // Update is called once per frame
    public void AddMagic()
    {
        if (staminaSlider.value != 100)
        {

            staminaSlider.value += 1;

            playerInventory.currentStamina += 1;
            //staminaSlider.value = playerInventory.currentStamina;
        }

        if (staminaSlider.value > staminaSlider.maxValue)
        {
            staminaSlider.value = staminaSlider.maxValue;
            playerInventory.currentStamina = playerInventory.maxStamina;
        }
    }

    public void DecreaseMagic()
    {
         staminaSlider.value -= decrease;
        playerInventory.currentStamina -= decrease;
        //staminaSlider.value = playerInventory.currentStamina;

        if (staminaSlider.value < 0)
        {
            staminaSlider.value = 0;
            playerInventory.currentStamina = 0;
        }
    }
}
