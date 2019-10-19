using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehaviour : Enemy
{
    public Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    //public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        //animimator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        //anim.SetBool("Walk", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
        myRigidbody.velocity = Vector2.zero;
    }

    public virtual void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
                //changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

                ChangeState(EnemyState.walk);
                //anim.SetBool("Walk", true);
            }

        }
        /*
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("Walk", false);
        }
        */

    }
    /*
    public void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("Horizontal", setVector.x);
        anim.SetFloat("Vertical", setVector.y);
    }

    public void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("Horizontal", setVector.x);
        anim.SetFloat("Vertical", setVector.y);
    }
    public void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }
    */
    public void ChangeState(EnemyState newstate)
    {
        if (currentState != newstate)
        {
            currentState = newstate;
        }

    }
}
