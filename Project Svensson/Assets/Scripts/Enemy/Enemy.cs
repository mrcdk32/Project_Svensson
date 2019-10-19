using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    [Header("State")]
    public EnemyState currentState;
    [Header("Enemy Data")]
    public float enemySpeed;
    public Vector2 homePos;
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;

    void OnEnable()
    {
        transform.position = homePos;
        currentState = EnemyState.idle;
    }
    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCoroutine(myRigidbody, knockTime));
    }
    private IEnumerator KnockCoroutine(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.stagger;
            myRigidbody.velocity = Vector2.zero;
        }

    }

}
