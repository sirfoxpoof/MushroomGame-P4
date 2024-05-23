using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{

    [SerializeField] Enemy enemyMove;

    private void Start()
    {
        enemyMove = gameObject.GetComponentInChildren<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
          
           enemyMove.inSightRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            enemyMove.inSightRange= false;
        }
    }
}
