using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Maus : Enemy
{
    [SerializeField] ParticleSystem shockWave;
    [SerializeField]float shockWaveTime;

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

            randomState = Random.Range(0, currentState);
            Debug.Log(randomState.ToString());

            CheckAttackState();
            //enemyAnimator.SetTrigger("TailLeft");
            Invoke("ResetAttack", resetTime);
            attacking = true;
        }
    
    }

    void CheckAttackState()
    {
        if(randomState == 0)
        {
            //first attack
            Debug.Log("FIRST");
        }
        if(randomState == 1)
        {
            //secodn attack
            enemyAnimator.SetTrigger("TailLeft");
        }
        if(randomState == 2)
        {
            //third attack
            enemyAnimator.SetTrigger("ShockWave");

            StartCoroutine("ShockWaveTime");
        }
    }

    IEnumerator ShockWaveTime()
    {
        yield return new WaitForSeconds(shockWaveTime);
        shockWave.Play();
    }
    private void ResetAttack()
    {
        attacking = false;
        gameObject.GetComponent<NavMeshAgent>().speed = 2;
    }
}