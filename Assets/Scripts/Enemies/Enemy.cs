using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent Agent;
    Transform player;

    public LayerMask getGround, getPlayer;

    //maybe patrolling

    //attacking
    float timeBetweenAttacks;
    bool attacking;

    //states
    [SerializeField]float sightRange, attackRange;
    public bool inSightRange, inAttackRange;

    //stats
    public float health, damage;
    public string enemyName;

    public Material normalColour;

    private void Start()
    {
        player = GameObject.Find("PlayerHolder").transform;
        Agent = gameObject.GetComponent<NavMeshAgent>();

        normalColour = gameObject.GetComponent<MeshRenderer>().material;
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
        Agent.SetDestination(player.position);

    }

    public void AttackPlayer()
    {
        //whenever the boss is attacking the boss cannot move
        Agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!attacking)
        {
            //hier attacked hij HOWEVER ik heb daar een ander script voor
            //miss gooi ik het heir ook wel in
            // is miss wel handig 
            /*nouja in ieder geval
             de bedoeling is dat hij hier attacks uit gaat voeren en de animaties doet enzo en hutsafluts*/


            attacking = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        attacking = false;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine("ChangeMaterial");
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log(enemyName + "DIED");
        }
    }
    IEnumerator ChangeMaterial()
    {
        Debug.Log("HUTS");
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }

}
