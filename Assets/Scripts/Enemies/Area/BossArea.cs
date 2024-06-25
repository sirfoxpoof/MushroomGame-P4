using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{

    [SerializeField] Maus enemyMove;

    private void Start()
    {
        enemyMove = gameObject.GetComponentInChildren<Maus>();
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
