using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("ToFill")]
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
    public float health, damage;
    public string enemyName;
    public float resetTime = 5;
    
    bool playerPosActive;

    public Slider healthSlider;

    public Material normalColour;
    

    private void Start()
    {
        healthSlider.maxValue = health;
        player = GameObject.Find("PlayerHolder").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        //zorgde voor errors :')
        //normalColour = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
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

    //check voor hoe groot de attackrange is
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    public virtual void AttackPlayer()
    {
        
        agent.SetDestination(transform.position);

        playerPosActive = true;
        var playerPos = player.position;
        playerPos.y = transform.position.y;


        //transform.LookAt(playerPos);
        /* if (!playerPosActive && !attacking)
         {
             Invoke("SetPlayerPos", 1);
             playerPosActive = true;
         }*/


        //transform.rotation = Quaternion.LookRotation(playerPos);
        /*if (!attacking)
        {
            //hier attacked hij HOWEVER ik heb daar een ander script voor
            //miss gooi ik het heir ook wel in
            // is miss wel handig 
            *//*nouja in ieder geval
             de bedoeling is dat hij hier attacks uit gaat voeren en de animaties doet enzo en hutsafluts*//*

            //enemyAnimator.SetTrigger("TailRight");
            Debug.Log(" AAAAAAH");
            //player.transform.GetComponent<Attack>().TakeDamage(damage);

            Invoke("ResetAttack", 5);
            attacking = true;
        }*/
    }


    /*private void ResetAttack()
    {
        attacking = false;
        gameObject.GetComponent<NavMeshAgent>().speed = 2;
    }*/
   /* public void SetPlayerPos()
    {
        playerPosActive = true;
        var playerPos = player.position;
        playerPos.y = transform.position.y;

        transform.LookAt(playerPos);
    }*/

    public void TakeDamage(float damage)
    {
        StartCoroutine("ChangeMaterial");
        health -= damage;
        
        healthSlider.value = health;

        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log(enemyName + " DIED");
            SceneManager.LoadScene(0);
            //Invoke("Reset", 2);
        }
        
    }

    /*public void Reset()
    {
        SceneManager.LoadScene(0);
    }*/

    IEnumerator ChangeMaterial()
    {
        Debug.Log("HUTS");
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = normalColour.color;
    }

}
