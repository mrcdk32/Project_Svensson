using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;
    [SerializeField] private string Tag;
    //  public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.isTrigger)
        {
            Rigidbody2D hit = collision.GetComponentInParent<Rigidbody2D>();
            if (hit != null)
            {
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(hit, knockTime);
                }



                if (collision.GetComponentInParent<Player>().currentState != PlayerState.stagger)
                {
                    hit.GetComponent<Player>().currentState = PlayerState.stagger;
                    collision.GetComponentInParent<Player>().Knock(knockTime);
                }



            }
        }
    }
}
