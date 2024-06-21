using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintCollider : MonoBehaviour
{
    [SerializeField] float sprintDamage;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("AAAAAAAAAH");
            other.GetComponent<Attack>().TakeDamage(sprintDamage);
        }
    }
}
