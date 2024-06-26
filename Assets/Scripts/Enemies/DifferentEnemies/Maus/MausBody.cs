using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MausBody : MonoBehaviour
{
    GameObject player;
    public bool onBody;
   
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            onBody = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            onBody = false;
        }
    }
    
}
