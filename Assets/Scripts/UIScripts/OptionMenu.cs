using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] bool settingsAan = false;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] PlayerMovement moveScript;

    [SerializeField] GameObject[] secondaryGameObjects;
    [SerializeField] GameObject[] primaryGameObjects;

    public void DoSettingsMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!settingsAan)
            {
                SettingsMenuOn();
            }
            else
            {
                SettingsMenuOff();

            }
            settingsAan = !settingsAan;
        }
    }

    /*public void DoSettingsMenuButton()
    {

        if (!settingsAan)
        {
            SettingsMenuOn();
        }
        else
        {
            SettingsMenuOff();

        }
        settingsAan = !settingsAan;
    }*/

    public void SettingsMenuOn()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;

        settingsMenu.gameObject.SetActive(true);
        
        moveScript.enabled = false;

    }

    public void SettingsMenuOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;

        SortMenu();
        settingsMenu.gameObject.SetActive(false);

        moveScript.enabled = true;
        print("settingsOff");
        
    }

    public void SortMenu()
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
        print("sortedMenu");
    }

        

        

}
/* muis moet stoppen met lopen
        * als settingsmenu uit is, moet ie aan
        * als settingsmenu al aan is, moet ie uit
        * 
       */
