using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Maus : Enemy
{
    [SerializeField] ParticleSystem shockWave;


    private void Start()
    {
        shockWave = GetComponentInChildren<ParticleSystem>();
    }
    public override void AttackPlayer()
    {

        //hier elke keer een random.range in doen, die moet gekoppeld zijn aan attacks
        //dan in de update checken welke eruit is gekomen

        //als de muis op een bepaalde health is dan krijtg hij een andere state en wordt er een index aan de random.range toegevoegd
        //dan gaat hij weer een random.range doen met die extra index en kan hij zo extra attacks doen
        base.AttackPlayer();
        if (!attacking)
        {
            //enemyAnimator.SetTrigger("TailLeft");
            shockWave.Play();
            Invoke("ResetAttack", resetTime);
            attacking = true;
        }
    
    }
    private void ResetAttack()
    {
        attacking = false;
        gameObject.GetComponent<NavMeshAgent>().speed = 2;
    }
}