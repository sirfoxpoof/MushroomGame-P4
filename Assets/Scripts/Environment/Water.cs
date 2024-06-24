using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{
    GameObject player;
   [SerializeField] float waterdamage;
   
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            player.GetComponent<Attack>().waterDamage = true;
            Invoke("TakeDamage", 0.05f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Attack>().waterDamage = false;
        }
    }

    private void TakeDamage()
    {
        player.GetComponent<Attack>().TakeDamage(waterdamage);
    }
}
