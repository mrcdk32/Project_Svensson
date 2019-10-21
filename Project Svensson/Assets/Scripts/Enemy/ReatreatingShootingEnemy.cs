using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReatreatingShootingEnemy : Enemy
{
  
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;
    private float waitTime;
    public float startWaitTime;
    public Transform moveSpot;
    public float maxX;
    public float minX;
    public float minY;
    public float maxY;
    public Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        myRigidbody = GetComponent<Rigidbody2D>();
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
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
        myRigidbody.velocity = Vector2.zero;

        CheckDistance();
    }
    public void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, target.position) < stoppingDistance && Vector2.Distance(transform.position, target.position) > retreatDistance)
            {
                transform.position = this.transform.position;
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
            else if (Vector2.Distance(transform.position, target.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, -enemySpeed * Time.deltaTime);
            }
  

        }
        else
        {
            Patrol();
        }
        /*else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            animation.SetBool("Walk", false);
        }
        */

    }
    public void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, enemySpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    public void ChangeState(EnemyState newstate)
    {
        if (currentState != newstate)
        {
            currentState = newstate;
        }

    }
}
