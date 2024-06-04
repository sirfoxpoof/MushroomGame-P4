using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{

    //player stats I guess
    public float health;
    float maxHealth = 100;
    public Weapons weapons;
    public PlayerMovement playerMovement;
    public Slider healthSlider;

    public Animator attackAnimator;

    [SerializeField]bool allowedAttack, attacking;
    //dodge
    float dodgeValue = 0.01f;

    bool takingDamage;

    private void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        playerMovement = GetComponent<PlayerMovement>();
        allowedAttack = true;
    }


    private void Update()
    {
        if (attacking)
        {
             if(weapons.inRange && weapons.enemy != null)
             {
                    attacking = false;
                    //weapons.gameObject.GetComponent<CapsuleCollider>().enabled = true;    
                    Debug.Log("supposed to do damage");
                    weapons.enemy.GetComponentInParent<Enemy>().TakeDamage(weapons.damage);
                   // StartCoroutine("AttackTime");
                    Invoke("Reset", 1.1f);
             }
            
        }
    }

    private void Reset()
    {
        weapons.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        attacking = false;
    }
    //hier moeten we checken welk wapen we vast hebben en het script vahn dat wapen pakken om die attack uit te voeren
    //verder spreken we dan de attack functie uit van dat wapen
    public void Attacking(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (allowedAttack)
            {
                weapons.gameObject.GetComponent<CapsuleCollider>().enabled = true;
                attackAnimator.Play("Attack");
                allowedAttack = false;
                attacking= true;
                StartCoroutine("AttackTime");
            }
        }
    }


    IEnumerator AttackTime()
    {
        allowedAttack = false;
        yield return new WaitForSeconds(1.1f);
        allowedAttack = true;
        weapons.gameObject.GetComponent<CapsuleCollider>().enabled = false;

    }

    public void TakeDamage(float damage)
    {
        //de character zichzelf damage geven

        if(!takingDamage)
        {
            health -= damage;
            StartCoroutine("DamageColour");
            takingDamage = true;
            healthSlider.value = health;
        }

        if(health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Heal(float healAmmount)
    {
        if (health < maxHealth)
        {
            health += healAmmount;
            healthSlider.value = health;
        }
        else
            return;
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
   /* public void DodgeLeft(InputAction.CallbackContext context)
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
    }*/
}
