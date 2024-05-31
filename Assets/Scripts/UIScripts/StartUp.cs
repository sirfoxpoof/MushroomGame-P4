using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    [SerializeField] GameObject[] secondaryGameObjects;
    [SerializeField] GameObject[] primaryGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        secondaryGameObjects = GameObject.FindGameObjectsWithTag("Secondary");
        primaryGameObjects = GameObject.FindGameObjectsWithTag("Primary");


        foreach (GameObject gameObject in secondaryGameObjects)
        {
            gameObject.SetActive(false);
        }

        foreach (GameObject gameObject in primaryGameObjects)
        {
            gameObject.SetActive(true);
        }
    }
}
