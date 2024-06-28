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
    public Tutorial tutorial;
    public Slider healthSlider;
    [SerializeField] GameObject deathScreen, hud;

    public Animator attackAnimator;

    [SerializeField] bool allowedAttack, attacking;
    public bool heavyAttacking;
    //dodge
    //float dodgeValue = 0.01f;

    bool takingDamage;
    [HideInInspector] public bool waterDamage;

    public void StartAttack()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        playerMovement = GetComponent<PlayerMovement>();
        allowedAttack = true;

        weapons.normalDamage = weapons.damage;
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
                    weapons.enemy.GetComponentInParent<Maus>().TakeDamage(weapons.damage);
                   // StartCoroutine("AttackTime");
                    Invoke("Reset", 2.3f);
             }
            
        }
    }

    private void Reset()
    {
        weapons.gameObject.GetComponent<BoxCollider>().enabled = false;
        attacking = false;
    }
    //hier moeten we checken welk wapen we vast hebben en het script vahn dat wapen pakken om die attack uit te voeren
    //verder spreken we dan de attack functie uit van dat wapen
    public void Attacking(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(allowedAttack && playerMovement.jumping)
            {
                weapons.damage = weapons.critDamage;
                weapons.gameObject.GetComponent<BoxCollider>().enabled = true;

                attackAnimator.Play("Heavy Attack");
                allowedAttack = false;
                attacking = true;

                StartCoroutine("AttackTime");
            }
            else if (allowedAttack)
            {
                weapons.damage = weapons.normalDamage;
                weapons.gameObject.GetComponent<BoxCollider>().enabled = true;

                attackAnimator.Play("Attack");
                allowedAttack = false;
                attacking = true;

                StartCoroutine("AttackTime");
            }
        }
    }


    IEnumerator AttackTime()
    {
        allowedAttack = false;
        yield return new WaitForSeconds(2.3f);
        allowedAttack = true;
        weapons.gameObject.GetComponent<BoxCollider>().enabled = false;

    }

    public void TakeDamage(float damage)
    {
        //de character zichzelf damage geven

        if(!takingDamage && !waterDamage)
        {
            health -= damage;
            StartCoroutine("DamageColour");
            takingDamage = true;
            healthSlider.value = health;
        }
        else if(waterDamage) 
        {
            health -= damage;
            healthSlider.value = health;
        }

        if(health <= 0)
        {
            attackAnimator.SetTrigger("Die");
            playerMovement.enabled = false;

            Invoke("Death", 5);

        }
    }

    public void Death()
    {
        deathScreen.SetActive(true);
        hud.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;

        tutorial.tutorialEnabled = false;

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
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1.2f);
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
        takingDamage = false;
    }
}
