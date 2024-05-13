using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float range, damage;
    public bool attacking = false;
    private bool inRange = false;
    GameObject enemy;
    //public Animator weaponAnimator;

    // Start is called before the first frame update


    // Update is called once per frame
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
        }
    }

    public void Attack()
    {
        if (attacking && inRange)
        {
            Destroy(enemy.gameObject);
        }

        attacking = false;
        inRange = false;
        //heel leeg
    }
}