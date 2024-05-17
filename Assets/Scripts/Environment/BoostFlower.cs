using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostFlower : MonoBehaviour
{
    private GameObject playerBody;
    private PlayerMovement player;

    [SerializeField] float flowerJump;

    [SerializeField] Animator flowerAnimator, playerAnimator;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerBody = collision.gameObject;
            player = playerBody.GetComponent<PlayerMovement>();
            playerAnimator = playerBody.GetComponentInChildren<Animator>();


            flowerAnimator.SetTrigger("Jump");
            playerAnimator.SetTrigger("Jumping");
            player.rb.AddForce(player.jump * flowerJump, ForceMode.Impulse);


            player.jumping = true;
        }
    }
}
