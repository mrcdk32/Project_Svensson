using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingEnemyTest : Enemy
{
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
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    { 
        myRigidbody.velocity = Vector2.zero;

        CheckDistance();


    }
    public void CheckDistance()
    {

        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
                //changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

                //ChangeState(EnemyState.walk);
                //anim.SetBool("Walk", true);
            }

        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            Patrol();
        }
        /*
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("Walk", false);
        }
        */

    }
    public void ChangeState(EnemyState newstate)
    {
        if (currentState != newstate)
        {
            currentState = newstate;
        }

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
}
