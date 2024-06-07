using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] bool settingsAan = false, firsttime = false;
    [SerializeField] GameObject settingsMenu, hud;
    [SerializeField] PlayerMovement moveScript;

    [SerializeField] StartUp mainmenu;
    [SerializeField] GameObject[] secondaryGameObjects;
    [SerializeField] GameObject[] primaryGameObjects;

    public void DoSettingsMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(mainmenu.mainMenuOn);
            if(!mainmenu.mainMenuOn)
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
        else
        {
            return;
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
        

        SortMenu();
        hud.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);

        moveScript.enabled = false;
        print("settingsOn");

    }

    public void SettingsMenuOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;

        SortMenu();
        settingsMenu.gameObject.SetActive(false);
        hud.gameObject.SetActive(true);

        moveScript.enabled = true;
        print("settingsOff");

    }

    public void SettingsButton()
    {
        if (!mainmenu.mainMenuOn)
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

    public void SortMenu()
    {
        if (!firsttime)
        {
            secondaryGameObjects = GameObject.FindGameObjectsWithTag("Secondary");
            primaryGameObjects = GameObject.FindGameObjectsWithTag("Primary");

            firsttime = true;
        }
        else
        {
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

}
/* muis moet stoppen met lopen
        * als settingsmenu uit is, moet ie aan
        * als settingsmenu al aan is, moet ie uit
        * 
       */
