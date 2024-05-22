using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    [SerializeField] GameObject[] secondaryGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        secondaryGameObjects = GameObject.FindGameObjectsWithTag("Secondary");

        if (secondaryGameObjects.Length == 0)
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAH");
        }

        foreach (GameObject gameObject in secondaryGameObjects)
        {
            gameObject.SetActive(false);
        }
    }
}
