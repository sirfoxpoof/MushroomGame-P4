using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Maus : Enemy
{
    /* bool charging;

     private void Update()
     {
         if (!charging && attacking)
         {

             agent.SetDestination(transform.forward);
             gameObject.GetComponent<NavMeshAgent>().speed = 10;

             Invoke("ChargeAttack", 5);
             charging = true;
         }
     }
     public void ChargeAttack()
     {
         charging = false;
     }*/


    private void Start()
    {
        StartCoroutine("Rotate", 2);
    }
    IEnumerator Rotate(float duration)
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }
    }
}