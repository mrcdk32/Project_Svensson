using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubber : MonoBehaviour
{
    public float speed;
    public Renderer rend;
    public Collider2D explosion;
    public Rigidbody2D myRigidbody;
    private Animator animation;
    public float lifetime;
    private float lifetimeSeconds;
    public float Cost;
    // Start is called before the first frame update
    public void Setup(Vector2 vel, Vector3 dir)
    {
        myRigidbody.velocity = vel.normalized * speed;
        transform.rotation = Quaternion.Euler(dir);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            StartCoroutine(Wait());
            explosion.enabled = true;
        }
    }
    void Start()
    {
        explosion.enabled = false;
        rend = GetComponent<Renderer>();
        explosion = GetComponent<Collider2D>();
        animation = GetComponent<Animator>();

        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;
    }

    // Update is called once per frame
    void Update()
    {


        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
        {

            StartCoroutine(Wait());
            explosion.enabled = true;


        }
    }

 

    IEnumerator Wait()
    {

        animation.SetBool("Explode", true);
        myRigidbody.velocity = Vector3.zero;

        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
        Debug.Log("Boom");
    }

}
