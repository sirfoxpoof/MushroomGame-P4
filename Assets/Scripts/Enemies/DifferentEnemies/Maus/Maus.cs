using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Maus : Enemy
{

    public override void AttackPlayer()
    {
        base.AttackPlayer();
        if (!attacking)
        {
            enemyAnimator.SetTrigger("TailLeft");
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