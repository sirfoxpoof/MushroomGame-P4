using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealShroom : MonoBehaviour
{

    [SerializeField]float healAmmount;
    [SerializeField]float healspeed;
    bool isHealing;

    GameObject player;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject;

            if(!isHealing )
            {
                Invoke("Healing", healspeed);
                isHealing = true;
            }
        }
    }

    void Healing()
    {
        player.GetComponent<Attack>().Heal(healAmmount);
        isHealing = false;
    }
}
