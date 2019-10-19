using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerhealth : BasicHealthScript
{
    [SerializeField] private Signal_Event healthSignal;

    void Start()
    {
        currentHealth = maxHealth.RuntimeValue;
    }
    public override void Damage(float amountToDamage)
    {
        base.Damage(amountToDamage);
        maxHealth.RuntimeValue = currentHealth;
        healthSignal.Raise();
        if (currentHealth == 0)
        {
            Body.SetActive(false);
        }
    }
    
}
