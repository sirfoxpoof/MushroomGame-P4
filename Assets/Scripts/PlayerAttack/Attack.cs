using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{

    //player stats I guess
    public float health;

    public Weapons weapons;
    public Animator attackAnimator;

   

    //hier moeten we checken welk wapen we vast hebben en het script vahn dat wapen pakken om die attack uit te voeren
    //verder spreken we dan de attack functie uit van dat wapen
    public void Attacking(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            attackAnimator.Play("Attack"); 
            weapons.attacking = true;

            if (weapons.attacking && weapons.inRange)
            {
                //hier damage doen
                Destroy(weapons.enemy.gameObject);
            }

            weapons.attacking = false;
            weapons.inRange = false;
        }
    }
}
