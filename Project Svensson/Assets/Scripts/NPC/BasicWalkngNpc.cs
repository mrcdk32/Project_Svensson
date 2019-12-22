using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWalkngNpc : DialogTrigger

{
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Animator anim;
    public Collider2D bounds;
    public float minMoveTime;
    public float maxMoveTime;
    private float moveTimeSeconds;
    private bool isMoving;
    public float minWaitTime;
    public float maxWaitTime;
    private float waitTimeSeconds;
    // Start is called before the first frame update
    void Start()
    {
        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        myRigidbody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        ChooseNewDirection();
    }

    // Update is called once per frame
    public override void Update()
    {

        base.Update();
        if (isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;
            if (moveTimeSeconds <= 0)
            {

                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                directionVector = Vector3.zero;
                anim.SetBool("moving", false);
                isMoving = false;
                ChangeDirection();
            }
            if (!playerInRange)
            {
                Move();
            }
        }
        else
        {
            waitTimeSeconds -= Time.deltaTime;
            if (waitTimeSeconds <= 0)
            {
                isMoving = true;
                anim.SetBool("moving", true);
                waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
            }
        }

    }
    private void ChooseNewDirection()
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
        if (bounds.bounds.Contains(temp))
        {
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }

    }
    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                directionVector = Vector3.right;
                break;
            case 1:
                directionVector = Vector3.up;
                break;
            case 2:
                directionVector = Vector3.left;
                break;
            case 3:
                directionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }
    void UpdateAnimation()
    {
        if (directionVector != Vector3.zero)
        {
            anim.SetFloat("Horizontal", directionVector.y);
            anim.SetFloat("Vertical", directionVector.x);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChooseNewDirection();
    }
}
