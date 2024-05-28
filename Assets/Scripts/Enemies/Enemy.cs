using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask getGround, getPlayer;

    //maybe patrolling

    //attacking
    float timeBetweenAttacks;
    public bool attacking, charge;

    //states
    [SerializeField]float sightRange, attackRange;
    [HideInInspector]public bool inSightRange, inAttackRange;

    //stats
    public float health, damage;
    public string enemyName;

    public Material normalColour;

    private void Start()
    {
        player = GameObject.Find("PlayerHolder").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();

        //normalColour = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;

        
    }

    private void Update()
    {
        // Check if the enemy can see 
        // if this is shit I'll use a sphere to check

        inAttackRange = Physics.CheckSphere(transform.position, attackRange, getPlayer);
       
       /* Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, 100))
        {

        }*/

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
        //whenever the boss is attacking the boss cannot move
        var playerPos = player.position;
        playerPos.y = transform.position.y;
  
        agent.SetDestination(transform.position);
        transform.LookAt(playerPos);
       
        if (!attacking)
        {
            //hier attacked hij HOWEVER ik heb daar een ander script voor
            //miss gooi ik het heir ook wel in
            // is miss wel handig 
            /*nouja in ieder geval
             de bedoeling is dat hij hier attacks uit gaat voeren en de animaties doet enzo en hutsafluts*/
            Debug.Log(" AAAAAAH");
            player.transform.GetComponent<Attack>().TakeDamage(damage);

            Invoke("ResetAttack", 5);
            attacking = true;
        }
    }
    private void ResetAttack()
    {
        attacking = false;
        gameObject.GetComponent<NavMeshAgent>().speed = 2;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine("ChangeMaterial");
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log(enemyName + " DIED");
        }
    }
    IEnumerator ChangeMaterial()
    {
        Debug.Log("HUTS");
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.clear;
    }

}
