using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MausBody : MonoBehaviour
{
    GameObject player;
    [SerializeField]float bodyDamage;
   

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Player"))
        {
            player = other.gameObject;
            Invoke("BodyDamage", 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Player"))
        {
            CancelInvoke("BodyDamage");
        }
    }
    void BodyDamage()
    {
        player.GetComponent<Attack>().TakeDamage(bodyDamage);
    }
}
