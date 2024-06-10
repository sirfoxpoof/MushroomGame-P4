using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Maus : Enemy
{
    [Header("Maus Values")]
    [SerializeField] ParticleSystem shockWave;
    [SerializeField] float shockWaveTime, speed, normalSpeed;

    bool speedAttacking;

    public AudioSource honk;
    private void Start()
    {
        shockWave = GetComponentInChildren<ParticleSystem>();
        speedAttacking = false;
    }
    public override void AttackPlayer()
    {
        base.AttackPlayer();
        if (!attacking)
        {
            playerPosActive = true;
            playerPos = player.position;

            randomState = Random.Range(0, currentState);
            Debug.Log(randomState.ToString());

            CheckAttackState();
            //enemyAnimator.SetTrigger("TailLeft");
            Invoke("ResetAttack", resetTime);
            attacking = true;
        }

        if (speedAttacking)
        {
            
            agent.SetDestination(playerPos);
            agent.speed = speed;
        }
    }

    void CheckAttackState()
    {
        if(randomState == 0)
        {
            //first attack
            honk.Play();
            Debug.Log("FIRST");
            speedAttacking = true;
            Invoke("ResetAttack", resetTime);
        }
        if (randomState == 1)
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
        speedAttacking = false;
        gameObject.GetComponent<NavMeshAgent>().speed = normalSpeed;
    }
}