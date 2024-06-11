using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MausBalls : MonoBehaviour
{

    //shit voor de poison spots, elke bal health geven
    //als de health op 0 is dan moet de spot stuk
    // als de spot stuk is dan damage doen aan muis
    //
    Maus mausScript;

    void StartUp()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {

        }
    }
}
