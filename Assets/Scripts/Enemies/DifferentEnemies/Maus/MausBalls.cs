using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MausBalls : MonoBehaviour
{

    //shit voor de poison spots, elke bal health geven
    //als de health op 0 is dan moet de spot stuk
    // als de spot stuk is dan damage doen aan muis
    //
    [SerializeField] float health;
    GameObject weapon, playerBody;
    [SerializeField] PlayerMovement player;

    [SerializeField]Maus mausScript;
    [SerializeField] Animator mausAnimator;
    [SerializeField] float ballDamage, throwAmount;
    Vector3 playerThrow = new Vector3 (0, 0, -10);

    [SerializeField]Material normalColour;

    [SerializeField] bool takingDamage;

    private void Start()
    {
        takingDamage = false;
        mausScript = GetComponentInParent<Maus>();
        normalColour = gameObject.GetComponent<Material>();
        
    }
   


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            weapon = other.gameObject;
            /*playerBody = other.gameObject;

            player = playerBody.GetComponent<PlayerMovement>();*/

            Debug.Log("AAAAAAAAAAAAAAAAH");
            TakeBallDamage();
        }
    }


    void TakeBallDamage()
    {
        if (!takingDamage)
        {

        }
            health -= weapon.GetComponent<Weapons>().damage;
            StartCoroutine("ChangeMaterial");
           // takingDamage = true;

        if(health <= 0)
        {
            mausAnimator.SetTrigger("Throw");


            player.rb.AddForce(playerThrow * throwAmount, ForceMode.Impulse);

            mausScript.TakeDamage(ballDamage);
            Destroy(gameObject);


        }

    }

    IEnumerator ChangeMaterial()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = normalColour.color;
        //takingDamage = false;
    }
}
