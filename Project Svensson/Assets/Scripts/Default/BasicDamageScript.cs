using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BasicDamageScript : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private string Tag;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tag) && collision.isTrigger)
        {
            BasicHealthScript temp = collision.GetComponent<BasicHealthScript>();
            if (temp)
            {
                temp.Damage(damage);
            }
        }
    }
}