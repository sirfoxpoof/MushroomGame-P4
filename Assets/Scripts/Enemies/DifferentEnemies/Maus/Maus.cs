using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Maus : Enemy
{
    [Header("Maus Values")]
    [SerializeField] ParticleSystem shockWave;
    [SerializeField] Transform speedAttackPoint;
    [SerializeField] float shockWaveTime, speed, normalSpeed;

    bool speedAttacking;
    float leftRight = 1;
    public AudioSource honk;

    [SerializeField] Collider sprintCollider;

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
            playerPos = speedAttackPoint.position;

            randomState = Random.Range(0, currentState);
            Debug.Log(randomState.ToString());

            CheckAttackState();
            //enemyAnimator.SetTrigger("TailLeft");
            Invoke("ResetAttack", resetTime);
            attacking = true;
        }

       /* if (speedAttacking)
        {
            
            agent.SetDestination(playerPos);
            agent.speed = speed;
        }*/
    }

    void CheckAttackState()
    {
        if (randomState == 0)
        {
            //first attack
            /* honk.Play();
             Debug.Log("FIRST");
             speedAttacking = true;
             Invoke("ResetAttack", resetTime);*/

            speedAttacking = true;
            agent.speed = speed;
            StartCoroutine("SprintAttack", playerPos);


        }
        if (randomState == 1)
        {
            //secodn attack
            leftRight = Random.Range(0, 2);

            if(leftRight == 0)
            {
                enemyAnimator.SetTrigger("TailLeft");
            }
            else if(leftRight == 1)
            {
                enemyAnimator.SetTrigger("TailRight");
            }
        }
        if(randomState == 2)
        {
            //third attack
            enemyAnimator.SetTrigger("ShockWave");

            StartCoroutine("ShockWaveTime");
        }
    }

    IEnumerator SprintAttack(Vector3 targetPos)
    {
        float step = 0.12f;

        while (speedAttacking)
        {
            sprintCollider.enabled = true;
            if (step >= (targetPos - transform.position).magnitude)
            {
                transform.position = targetPos;
                speedAttacking = false;
                break;
            }
            //transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            agent.SetDestination(targetPos);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        sprintCollider.enabled = false;
        
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