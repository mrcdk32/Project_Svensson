using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : BasicEnemyBehaviour
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    private void Update()
    {
        if (canFire == false)
        {
            fireDelaySeconds -= Time.deltaTime;
            if (fireDelaySeconds <= 0)
            {
                canFire = true;
                fireDelaySeconds = fireDelay;
            }
        }
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }
    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                if (canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<EnemyProjectile>().Launch(tempVector);
                    canFire = false;
                    ChangeState(EnemyState.walk);
                    //animation.SetBool("Walk", true);
                }
            }

        }
        /*else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            animation.SetBool("Walk", false);
        }
        */

    }
}
