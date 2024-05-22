using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    //EnemyStats
    public float health, damage;
    public string enemyName;

    public Material normalColour;

  /*  NavMeshAgent Agent;
    Transform player;

    public LayerMask getGround, getPlayer;

    //maybe patrolling

    //attacking
    float timeBetweenAttacks;
    bool attacking;

    //states
    float sightRange, attackRange;
    bool inSightRange, inAttackRange;*/


    //EnemyAttack
    private void Start()
    {
        normalColour = gameObject.GetComponent<MeshRenderer>().material;

        /*player = GameObject.Find("PlayerHolder").transform;
        Agent = GetComponent<NavMeshAgent>();*/
    }

   /* private void Update()
    {
        
    }
    public void Patrolling()
    {

    }

    public void ChasePlayer()
    {

    }*/

    public void Attack()
    {

    }
    //EnemyTakeDamage

    public void TakeDamage(float damage)
    {
        StartCoroutine("ChangeMaterial");
        health -= damage;
        if(health <= 0) 
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
