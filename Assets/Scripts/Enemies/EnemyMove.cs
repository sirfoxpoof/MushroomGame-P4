using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    NavMeshAgent Agent;
    Transform player;

    public LayerMask getGround, getPlayer;

    //maybe patrolling

    //attacking
    float timeBetweenAttacks;
    bool attacking;

    //states
    float sightRange, attackRange;
    bool inSightRange, inAttackRange;


    private void Start()
    {
        player = GameObject.Find("").transform;
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
    }
}
