using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{

    //player stats I guess
    public float health;

    public Weapons weapons;
    public PlayerMovement playerMovement;

    public Animator attackAnimator;

    //dodge
    Vector3 dodge = new Vector3(1000,0,0);
    float dodgeSpeed = 5;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

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

    public void TakeDamage()
    {
        //de character zichzelf damage geven
    }

    public void DodgeLeft(InputAction.CallbackContext context)
    {
        //ik wil de dodge hier ook in zetten want dat is makkelijker met aanspreken enzo met invincibility en whoooooooo

        //klote dodge
        if (context.performed)
        {
            playerMovement.rb.AddForce(Vector3.left * 10, ForceMode.Impulse);
        }
    }

    public void DodgeRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerMovement.rb.AddForce(Vector3.right * 10, ForceMode.Impulse);
        }
    }
}
