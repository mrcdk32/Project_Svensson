using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BasicHealthScript
{
    void Start()
    {
        currentHealth = maxHealth.initialValue;
    }
    public override void Damage(float amountToDamage)
    {


        base.Damage(amountToDamage);
        maxHealth.RuntimeValue = currentHealth;
        if (currentHealth == 0)
        {
            Dead();
        }
    }
}
