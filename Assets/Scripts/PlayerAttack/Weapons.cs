using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float damage, normalDamage,critDamage;
    public bool attacking = false;
    public bool inRange = false;
    public GameObject enemy;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.gameObject;
            inRange = true;
        }
        else
        {
            inRange = false;
            enemy = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        enemy = null;
    }
}
