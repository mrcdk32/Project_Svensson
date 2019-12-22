using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle,
    ability
}

public class Player : MonoBehaviour
{
    [Header("State")]
    public PlayerState currentState;
    [Header("Player Data")]
    public float speed;
    public float runSpeed;
    public float normalSpeed;
    public bool isRunning = false;
    private Rigidbody2D myRigidbody;
    public Vector3 move;
    private Animator animation;
    [SerializeField]
    private Transform minimapIndicator;
    [SerializeField] private GenericAbility currentAbility;
    public SpriteRenderer receivedsprite;
    public Image primaryImage;
    public Image secondaryImage;
    [Header("Value")]
    public VectorValue startingPosition;
    public Inventory inventory;
    public Signal_Event decreaseStamina;
    public Signal_Event addStamina;
    public Signal_Event decreaseAmmo;
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    [Header("Primary")]
    public Item Pencil;
    public Item bluePencil;
    [Header("Secondary")]
    public Item phone;
    public Item rubber;
    public GameObject phonePro;
    public GameObject rubberPro;
    [Header("Iframe")]
    public Color flashcolor;
    public Color regularcolor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;
    // Start is called before the first frame update

    private Vector2 facingDirection = Vector2.down;
    void Start()
    {
        animation = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == PlayerState.interact)
        {
            return;
        }
        move = Vector3.zero;
        move.x = Input.GetAxisRaw("Horizontal");
        if(move.x == 1)
        {
            minimapIndicator.eulerAngles = new Vector3(0, 0, 270);
        }
        if (move.x == -1)
        {
            minimapIndicator.eulerAngles = new Vector3(0, 0, 90);
        }
       
        move.y = Input.GetAxisRaw("Vertical");
        if (move.y == -1)
        {
            minimapIndicator.eulerAngles = new Vector3(0, 0, 180);
        }
        if (move.y == 1)
        {
            minimapIndicator.eulerAngles = new Vector3(0, 0, 0);
        }


        if (Input.GetButton("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            if (inventory.CheckForItem(Pencil))
            {
                StartCoroutine(AttackCoroutine());
            }
            if (inventory.CheckForItem(bluePencil))
            {
                StartCoroutine(BlueAttackCoroutine());
            }
          

        }
        if (Input.GetKeyDown("1"))
        {
            inventory.Switch();
        }
        else if (Input.GetButton("SecondaryAttack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            if (inventory.CheckForItem(phone))
            {
                if (inventory.phone != 0)
                {

                    StartCoroutine(PhoneAttackCoroutine());
                    decreaseAmmo.Raise();
                }
            }

            else if (inventory.CheckForItem(rubber) )
            {
                if (inventory.rubber != 0)
                {

                    StartCoroutine(RubberAttackCoroutine());
                    decreaseAmmo.Raise();
                }
            }
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
            if (Input.GetButton("Run") && currentState == PlayerState.walk && inventory.currentStamina > 0)
            {
                    isRunning = true;
                    speed = runSpeed;
                decreaseStamina.Raise();
            }

            else
            {

                isRunning = false;
                speed = normalSpeed;
                addStamina.Raise();

            }
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
            facingDirection = move;

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
    private IEnumerator BlueAttackCoroutine()
    {
        animation.SetBool("BlueAttack", true);
        currentState = PlayerState.attack;
        yield return null;
        animation.SetBool("BlueAttack", false);
        yield return new WaitForSeconds(1f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.idle;
        }
    }


 



    private IEnumerator PhoneAttackCoroutine()
    {
        //animation.SetBool("attack", true);
        currentState = PlayerState.attack;
        yield return null;
        MakePhone();
       // animation.SetBool("attack", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }
    private IEnumerator RubberAttackCoroutine()
    {
        //animation.SetBool("attack", true);
        currentState = PlayerState.attack;
        yield return null;
        MakeRubber();
        // animation.SetBool("attack", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }
    public IEnumerator AbilityCoroutine(float abilityDuration)
    {
        currentState = PlayerState.ability;
 
            currentAbility.Ability(transform.position, facingDirection, animation = GetComponent<Animator>(), myRigidbody);
            yield return null;
        yield return new WaitForSeconds(abilityDuration);
        currentState = PlayerState.idle;
    }


    private void MakePhone()
    {
        //if (playerinventory.currentMagic > 0)
        //{
            Vector2 temp = new Vector2(animation.GetFloat("Horizontal"), animation.GetFloat("Vertical"));
            phone Phone = Instantiate(phonePro, transform.position, Quaternion.identity).GetComponent<phone>();
            Phone.Setup(temp, ChooseProjectileDirection());
           // playerinventory.ReduceMagic(Arrow.magicCost);
           // reduceMagic.Raise();

        //}
    }
    private void MakeRubber()
    {
        //if (playerinventory.currentMagic > 0)
        //{
        Vector2 temp = new Vector2(animation.GetFloat("Horizontal"), animation.GetFloat("Vertical"));
        rubber Rubber = Instantiate(rubberPro, transform.position, Quaternion.identity).GetComponent<rubber>();
        Rubber.Setup(temp, ChooseProjectileDirection());
        // playerinventory.ReduceMagic(Arrow.magicCost);
        // reduceMagic.Raise();

        //}
    }

    Vector3 ChooseProjectileDirection()
    {
        float temp = Mathf.Atan2(animation.GetFloat("Vertical"), animation.GetFloat("Horizontal")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCoroutine(knockTime));
    }
    private IEnumerator KnockCoroutine(float knockTime)
    {
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCoroutine());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.walk;
            myRigidbody.velocity = Vector2.zero;

        }
    }

    public void RaiseItem()
    {
        if (inventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                Debug.Log("raise");
                currentState = PlayerState.interact;
                animation.SetBool("Recive Item", true);
                receivedsprite.sprite = inventory.currentItem.itemSprite;
            }
            else
            {
                animation.SetBool("Recive Item", false);
                currentState = PlayerState.idle;
                receivedsprite.sprite = null;
                inventory.currentItem = null;
                Debug.Log("Lower");
            }
        }
    }
    public void DisplayWeapon()
    {
        if(inventory.primaryWeapon[0] != null && inventory.secondaryWeapon[0] != null)
        {
          primaryImage.sprite = inventory.primaryWeapon[0].itemSprite;
            secondaryImage.sprite = inventory.secondaryWeapon[0].itemSprite;
        }
        else if (inventory.primaryWeapon[0] != null && inventory.secondaryWeapon[0] == null)
        {

            primaryImage.sprite = inventory.primaryWeapon[0].itemSprite;
            secondaryImage.sprite = null;

        }
        else
        {
            primaryImage.sprite = null;
            secondaryImage.sprite = null;
        }
    }
    private IEnumerator FlashCoroutine()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashcolor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularcolor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
}

