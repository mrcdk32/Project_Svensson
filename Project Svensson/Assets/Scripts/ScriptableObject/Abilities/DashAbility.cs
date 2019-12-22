using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Dash Ability", fileName = "Dash Ability")]

public class DashAbility : GenericAbility
{
    public float dashForce;

    public override void Ability(Vector3 playerPosition, Vector3 playerFacingDirection,
        Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
    {
        if(playerStamina.initialValue >= staminaCost)
        {
            playerStamina.initialValue -= staminaCost;
            usePlayerStamina.Raise();
        }
        else
        {
            return;
        }
        if(playerRigidbody)
        {
            Vector3 dashVector = playerRigidbody.transform.position + (Vector3)playerFacingDirection.normalized * dashForce;
            playerRigidbody.DOMove(dashVector, duration);
        }
    }
}
