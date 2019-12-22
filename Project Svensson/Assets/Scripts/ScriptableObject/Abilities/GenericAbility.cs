using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Generic Ability", fileName = "New Generic Ability")]
public class GenericAbility : ScriptableObject
{
    public float staminaCost;
    public float duration;

    public FloatValue playerStamina;
    public Signal_Event usePlayerStamina;

    public virtual void Ability(Vector3 playerPosition, Vector3 playerFacingDirection,
        Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
    {

    }

}
