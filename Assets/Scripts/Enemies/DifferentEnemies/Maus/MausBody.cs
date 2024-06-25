using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MausBody : MonoBehaviour
{
    GameObject player;
    public bool onBody;
   

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Player"))
        {
           onBody = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Player"))
        {
            onBody = false;
        }
    }
    
}
