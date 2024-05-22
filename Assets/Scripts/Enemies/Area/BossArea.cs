using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{

    EnemyMove enemyMove;

    private void Start()
    {
        enemyMove = GetComponentInChildren<EnemyMove>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
