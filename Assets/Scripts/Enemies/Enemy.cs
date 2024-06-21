using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Values")]
    
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask getGround, getPlayer;

    //maybe patrolling

    //attacking
    [Header("AttackValues")]
    public bool attacking;
    float timeBetweenAttacks;

    //states
    [SerializeField]float sightRange, attackRange;
    [HideInInspector]public bool inSightRange, inAttackRange;

    //stats
    [Header("Stats")]
    public Animator enemyAnimator;
    public float health, currentHealth, damage;
    public string enemyName;
    public float resetTime = 7;

    //TO HIDE JUST TO CHECK NOW
    public int currentState = 0;
    public int randomState = 0;

    [HideInInspector]public bool playerPosActive;
    [HideInInspector] public Vector3 playerPos;
    public Slider healthSlider;

    public Material normalColour;
    

    public void StartEnemy()
    {
        currentState = 0;
        randomState = 0;
        currentHealth = health;

        healthSlider.maxValue = currentHealth;
        player = GameObject.Find("PlayerHolder").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check if the player is in range to attack 
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, getPlayer);
       
        //states
        if(inSightRange && !inAttackRange)
            ChasePlayer();
        
        if (inSightRange && inAttackRange)
            AttackPlayer();

    }

    //check to see how big the attack range is
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
        enemyAnimator.SetTrigger("Walk");
    }
    
    //basic things for the attack, the actual attack happens inside the scripts that inherits from this one
    public virtual void AttackPlayer()
    {
        
        agent.SetDestination(transform.position);

        /*playerPosActive = true;
        var playerPos = player.position;
        playerPos.y = transform.position.y;*/

        //hier attacked hij HOWEVER ik heb daar een ander script voor
        //miss gooi ik het heir ook wel in
        // is miss wel handig 
        //*nouja in ieder geval
        //de bedoeling is dat hij hier attacks uit gaat voeren en de animaties doet enzo en hutsafluts*//*

        // dus de attack gewoon in MAus 
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine("ChangeMaterial");
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            //Destroy(gameObject);
            enemyAnimator.SetTrigger("Die");
            Debug.Log(enemyName + " DIED");
            //SceneManager.LoadScene(0);
            //Invoke("Reset", 2);
        }

        //Zet de currentstate naar boven als de health onder een bepaald procent komt.
        if (currentHealth / health <= 0.99f && currentState == 0)
        {
            //Statefunctie();
            currentState++;
            //randomState++;
        }
        else if (currentHealth / health <= 0.6f && currentState == 1)
        {
            //StateFunctie();
            currentState++;
            //randomState++;
        }
        else if (currentHealth / health <= 0.3f && currentState == 2)
        {
            //StateFunctie();
            currentState++;
            //randomState++;
        }

        
    }


    IEnumerator ChangeMaterial()
    {
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = normalColour.color;
    }

}
