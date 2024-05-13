using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostFlower : MonoBehaviour
{
    private GameObject playerBody;
    private PlayerMovement player;

    [SerializeField] float flowerJump;

    [SerializeField] Animator flowerAnimator;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerBody = collision.gameObject;
            player = playerBody.GetComponent<PlayerMovement>();


            flowerAnimator.SetTrigger("Jump");
            player.rb.AddForce(player.jump * flowerJump, ForceMode.Impulse);
        }
    }
}
