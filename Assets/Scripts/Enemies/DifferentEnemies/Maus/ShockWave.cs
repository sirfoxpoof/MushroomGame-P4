using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    [SerializeField] float shockWaveDamage;

    /*void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }*/

    /* void OnParticleCollision(GameObject other)
     {
         Debug.Log("First check");
         int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
         Rigidbody rb = other.GetComponent<Rigidbody>();
         int i = 0;

         while (i < numCollisionEvents)
         {
             Debug.Log("Secon check");
             if (rb)
             {
                 Debug.Log("lasty check");
                 Vector3 pos = collisionEvents[i].intersection;
                 Vector3 force = collisionEvents[i].velocity * 10;
                 rb.AddForce(force);
             }
             i++;

             *//*private void OnParticleCollision(GameObject other)
             {
                if(other.tag == "Player")
                {
                     other.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
                     Debug.Log("HITTING");
                } 
             }*//*

         }

     }*/

    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Attack>().TakeDamage(shockWaveDamage);
        }
    }
}