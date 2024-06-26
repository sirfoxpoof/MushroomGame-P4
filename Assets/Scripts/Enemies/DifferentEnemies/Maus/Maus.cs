using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Maus : MonoBehaviour
{
    public MausBody body;


    [SerializeField] ParticleSystem shockWave;
    [SerializeField] Transform speedAttackPoint, player;
    [SerializeField] float shockWaveTime, speed, normalSpeed, detectRange = 40, attackRange = 15;
    bool speedAttacking;
    float leftRight = 1;

    public AudioSource honk;
    public Animator animator;

    public NavMeshAgent agent;

    public LayerMask getGround, getPlayer;

    //maybe patrolling

    //attacking
    public bool attacking;
   // float timeBetweenAttacks;

    //states
    //[SerializeField] float sightRange, attackRange;
    [HideInInspector] public bool inSightRange, inAttackRange;

    //stats
    [Header("Stats")]
    public float health, currentHealth, damage;
    public string enemyName;
    public float resetTime = 7;

    int currentState = 0;
    int randomState = 0;

    [HideInInspector] public bool playerPosActive;
    [HideInInspector] public Vector3 playerPos;
   
    [SerializeField]public Slider healthSlider;
    public Material normalColour;

    GameObject targetTarget;


    public enum MausState
    {
        IDLE,
        GOTO,
        ATTACK,
        TIRED,
        DEAD,
        ONMAUS
    }

    public MausState state;

    [SerializeField] Collider sprintCollider;

    public void StartMaus()
    {
        state = MausState.IDLE;
        shockWave = GetComponentInChildren<ParticleSystem>();
        speedAttacking = false;

       
        currentState = 0;
        randomState = 0;
        currentHealth = health;

        //healthSlider.maxValue = currentHealth;
        player = GameObject.Find("PlayerHolder").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        switch (state)
        {
            case MausState.IDLE:
                Idle();
                break;
            case MausState.GOTO:
                Goto();
                break;
            case MausState.ATTACK:
                StartCoroutine(Attack());
                //Attack();
                break;
            case MausState.TIRED:
                StartCoroutine(Tired());
                //Tired();
                break;
            case MausState.DEAD:
                break;
            case MausState.ONMAUS:
                OnBody();
                break;

        }

        if(Vector3.Distance(player.position, transform.position) > detectRange)
        {
            state = MausState.IDLE;
        }

        if(body.onBody)
        {
            state = MausState.ONMAUS;
        }
    }


    public void Idle()
    {
        animator.Play("Idle");

        if (Vector3.Distance(player.position, transform.position) < detectRange && Vector3.Distance(player.position, transform.position) > attackRange)
        {
            inSightRange = true;
            state = MausState.GOTO;
            animator.SetTrigger("Walk");
        }
    }

    public void Goto()
    {
        agent.SetDestination(player.position);
        animator.SetTrigger("Walk");


        if(Vector3.Distance(player.position, transform.position) < attackRange)
        {
            state = MausState.ATTACK;
        }
    }

    public IEnumerator Attack()
    {
        if (!attacking)
        {

            attacking = true;
            StopAllCoroutines();




            //Calculates distance between mause and player, adds the attackdistance and spawns a target at the end.
            //after that the mause can run to the target.
            Vector3 heading = player.position - transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;

            float attackDistance = 20f;

            GameObject target = new GameObject();
            target.name = "Target";
            target.transform.position = transform.position + direction * attackDistance;

            playerPos = target.transform.position;
            Debug.DrawLine(transform.position, target.transform.position, Color.red, 10f);

            //+= transform.forward * Time.deltaTime * movementSpeed;

            agent.SetDestination(transform.position);
            /*playerPosActive = true;
            playerPos = speedAttackPoint.position;*/

            randomState = Random.Range(0, currentState);
             Debug.Log(randomState.ToString());

             CheckAttackState();
             Invoke("ResetAttack", resetTime);
        }

        /*if (speedAttacking)
        {
            agent.SetDestination(playerPos);
        }*/

             // random attack based on hp.
            // Dan terwijl anim aan het spelen is, wacht zolang de anim is en daarna doe Tired.
        yield return new WaitForSeconds(5);
        state = MausState.TIRED;
    }

    public IEnumerator Tired()
    {
        animator.SetTrigger("Tired");
        yield return new WaitForSeconds(10);
        // play tired anim en wacht.
        animator.SetTrigger("TiredDone");
        state = MausState.GOTO;
    }

    public void OnBody()
    {
        agent.SetDestination(transform.position);

        if (!body.onBody)
        {
            state = MausState.GOTO;
        }
    }

    void CheckAttackState()
    {
        if (randomState == 0)
        {
            animator.SetTrigger("Sprint");
            agent.SetDestination(transform.position);   
            //speedAttacking = true;
            agent.speed = speed;

        }
        if (randomState == 1)
        {
            //secodn attack
            leftRight = Random.Range(0, 2);

            if(leftRight == 0)
            {
                animator.SetTrigger("TailLeft");
            }
            else if(leftRight == 1)
            {
                animator.SetTrigger("TailRight");
            }
        }
        if(randomState == 2)
        {
            //third attack
            animator.SetTrigger("ShockWave");

            StartCoroutine("ShockWaveTime");
        }
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine("ChangeMaterial");
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        animator.Play("TakeDamage");

        if (currentHealth <= 0)
        {
            //Destroy(gameObject);
            animator.SetTrigger("Die");
            state = MausState.DEAD;
            Debug.Log(enemyName + " DIED");
            //SceneManager.LoadScene(0);
            //Invoke("Reset", 2);
        }

        //Zet de currentstate naar boven als de health onder een bepaald procent komt.
        if (currentHealth / health <= 0.99f && currentState == 0)
        {
            currentState++;
        }
        else if (currentHealth / health <= 0.6f && currentState == 1)
        {
            currentState++;
        }
        else if (currentHealth / health <= 0.3f && currentState == 2)
        {
            currentState++;
        }
    }

    IEnumerator ChangeMaterial()
    {
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = normalColour.color;
    }


    //public override void AttackPlayer()
    //{
    //    base.AttackPlayer();
    //    if (!attacking)
    //    {
    //        playerPosActive = true;
    //        playerPos = speedAttackPoint.position;

    //        randomState = Random.Range(0, currentState);
    //        Debug.Log(randomState.ToString());

    //        CheckAttackState();
    //        Invoke("ResetAttack", resetTime);
    //        attacking = true;
    //    }

    /* if (speedAttacking)
     {

         agent.SetDestination(playerPos);
         agent.speed = speed;
     }*/



   /* private void AnimBeforeSprintAttack()
    {
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("StartSprint") && enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            print("WTF");
            StartCoroutine("SprintAttack", playerPos);
        }
    }*/
    
    public void DoSprintAttack()
    {
        speedAttacking = true;
        StartCoroutine("SprintAttack", playerPos);
    }

    IEnumerator SprintAttack(Vector3 targetPos)
    {
        float step = 0.12f;

        while (speedAttacking)
        {
            agent.speed = speed;
            sprintCollider.enabled = true;
            // enemyAnimator.Play("Sprint");
            if (step >= (targetPos - transform.position).magnitude)
            {
                transform.position = targetPos;
                speedAttacking = false;
                break;
            }
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

        targetTarget = null;
        gameObject.GetComponent<NavMeshAgent>().speed = normalSpeed;
    }
}