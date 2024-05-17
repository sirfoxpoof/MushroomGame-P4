using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //EnemyStats
    public float health, damage;
    public string enemyName;

    public Material normalColour;
    //EnemyAttack
    private void Start()
    {
        normalColour = gameObject.GetComponent<MeshRenderer>().material;
    }
    public void Attack()
    {

    }
    //EnemyTakeDamage

    public void TakeDamage(float damage)
    {
        StartCoroutine("ChangeMaterial");
        health -= damage;
        if(health <= 0) 
        { 
            Destroy(gameObject);
            Debug.Log(enemyName + "DIED");
        }
    }

    IEnumerator ChangeMaterial()
    {
        Debug.Log("HUTS");
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
