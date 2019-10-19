using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHealthScript : MonoBehaviour
{
    public FloatValue maxHealth;
    public float currentHealth;
    public GameObject Body;

    // Start is called before the first frame update


    public virtual void Heal(float amountToHeal)
    {
        currentHealth += amountToHeal;
        if (currentHealth > maxHealth.RuntimeValue)
        {
            currentHealth = maxHealth.RuntimeValue;
        }
    }

    public virtual void FullHeal()
    {
        currentHealth = maxHealth.RuntimeValue;
    }
    public virtual void Damage(float amountToDamage)
    {
        currentHealth -= amountToDamage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public virtual void InstandDeath()
    {
        currentHealth = 0;
    }

    public virtual void Dead()
    {
        if (currentHealth == 0)
        {
            Destroy(Body.gameObject);
        }
    }
}
