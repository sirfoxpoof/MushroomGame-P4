using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartUp : MonoBehaviour
{
    [SerializeField] GameObject[] secondaryGameObjects;
    [SerializeField] GameObject[] primaryGameObjects;
    [SerializeField] GameObject[] menuGameObjects;
    [SerializeField] Enemy enemy;
    [SerializeField] Attack attack;

    public bool mainMenuOn;
    public GameObject hud;
    // Start is called before the first frame update
    void Start()
    {
        secondaryGameObjects = GameObject.FindGameObjectsWithTag("Secondary");
        primaryGameObjects = GameObject.FindGameObjectsWithTag("Primary");
        //menuGameObjects = GameObject.FindGameObjectsWithTag("Menu");

        mainMenuOn = true;

        foreach (GameObject gameObject in secondaryGameObjects)
        {
            gameObject.SetActive(false);
        }

        foreach (GameObject gameObject in primaryGameObjects)
        {
            gameObject.SetActive(true);
        }

        /*foreach (GameObject gameObject in menuGameObjects)
        {
            gameObject.SetActive(false);
        }*/

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void MenuOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainMenuOn = false;
        hud.SetActive(true);

        enemy.StartEnemy();
        attack.StartAttack();
    }

}
