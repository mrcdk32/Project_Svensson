using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class Player : MonoBehaviour
{
    [Header("State")]
    public PlayerState currentState;
    [Header("Player Data")]
    public float speed;
    private Rigidbody2D myRigidbody;
    public Vector3 move;
    private Animator animation;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Vector3.zero;
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCoroutine());

        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }
    void UpdateAnimationAndMove()
    {
        if (move != Vector3.zero)
        {
            MoveForce();
            animation.SetFloat("Horizontal", move.x);
            animation.SetFloat("Vertical", move.y);
            animation.SetBool("moving", true);

        }
        else
        {
            animation.SetBool("moving", false);
        }
    }
    void MoveForce()
    {
        move.Normalize();
        myRigidbody.MovePosition(transform.position + move * speed * Time.deltaTime);
    }
    private IEnumerator AttackCoroutine()
    {
        animation.SetBool("attack", true);
        currentState = PlayerState.attack;
        yield return null;
        animation.SetBool("attack", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }
    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCoroutine(knockTime));
    }
    private IEnumerator KnockCoroutine(float knockTime)
    {
        if (myRigidbody != null)
        {
            //StartCoroutine(FlashCoroutine());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;

        }
    }
}
