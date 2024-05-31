using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float range, damage;
    public bool attacking = false;
    public bool inRange = false;
    public GameObject enemy;
    //public Animator weaponAnimator;

    // Start is called before the first frame update


    // Update is called once per frame
    private void OnTriggerStay(Collider other)
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
