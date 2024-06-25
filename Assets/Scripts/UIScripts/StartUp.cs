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

    public GameObject deathScreen;
    [SerializeField] Maus enemy;
    [SerializeField] Attack attack;

    [SerializeField] Tutorial tutorial;     

    public bool mainMenuOn;
    public GameObject hud;
    // Start is called before the first frame update
    void Start()
    {
        secondaryGameObjects = GameObject.FindGameObjectsWithTag("Secondary");
        primaryGameObjects = GameObject.FindGameObjectsWithTag("Primary");
        //menuGameObjects = GameObject.FindGameObjectsWithTag("Menu");
        tutorial.tutorialEnabled = false;

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
        tutorial.tutorialEnabled = true;

        hud.SetActive(true);

        enemy.StartMaus();
        attack.StartAttack();
    }

    public void TryAgain()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        mainMenuOn = false;
        deathScreen.SetActive(false);
        hud.SetActive(true);

        enemy.StartMaus();
        attack.StartAttack();

    }

}
