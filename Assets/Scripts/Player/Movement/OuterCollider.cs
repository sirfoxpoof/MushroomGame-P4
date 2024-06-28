using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterCollider : MonoBehaviour
{
    public PlayerMovement playerMovement;

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("bool notJumpable = " + playerMovement.notJumpable);
            playerMovement.notJumpable = true;
        }
        else
        {
            playerMovement.notJumpable = false;
        }
    }
}
