using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{

    //player stats I guess
    public float health;

    public Weapons weapons;
    public PlayerMovement playerMovement;

    public Animator attackAnimator;

    [SerializeField]bool allowedAttack;
    //dodge
    float dodgeValue = 0.01f;

    bool takingDamage;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        allowedAttack = true;
    }

    //hier moeten we checken welk wapen we vast hebben en het script vahn dat wapen pakken om die attack uit te voeren
    //verder spreken we dan de attack functie uit van dat wapen
    public void Attacking(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            weapons.attacking = true;
            if(allowedAttack)
            {
                StartCoroutine("AttackTime");
                if (weapons.attacking && weapons.inRange && weapons.enemy != null)
                {
                    //hier damage doen
                    Debug.Log("supposed to do damage");
                    weapons.enemy.GetComponentInParent<Enemy>().TakeDamage(weapons.damage);

                }

            }
            attackAnimator.Play("Attack");
            weapons.attacking = false;
           // weapons.inRange = false;
        }
    }


    IEnumerator AttackTime()
    {
        allowedAttack = false;
        yield return new WaitForSeconds(1.5f);
        allowedAttack = true;
    }

    public void TakeDamage(float damage)
    {
        //de character zichzelf damage geven

        if(!takingDamage)
        {
            health -= damage;
            StartCoroutine("DamageColour");
            takingDamage = true;
        }

        if(health <= 0)
        {
            SceneManager.LoadScene("Level 1");
        }

    }

    IEnumerator DamageColour()
    {
        Debug.Log("TAKING DAMAGE");
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1.2f);
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
        takingDamage = false;
    }

    //dodgew aka wtf uis dit hoe kut wil jke het hebben
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
            //playerMovement.rb.AddForce(Vector3.right * 10, ForceMode.Impulse);
           transform.Translate(transform.position.x * dodgeValue, transform.position.y, 0);
        }
    }
}
