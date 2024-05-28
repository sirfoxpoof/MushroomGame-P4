using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MausTail : MonoBehaviour
{
    [SerializeField] Maus maus;
    float tailDamage = 10;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<Attack>().TakeDamage(tailDamage);
        }
    }
}
