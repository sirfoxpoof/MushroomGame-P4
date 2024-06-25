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
    [SerializeField] Tutorial tutorial;
    [SerializeField] GameObject[] secondaryGameObjects;
    [SerializeField] GameObject[] primaryGameObjects;

    public void DoSettingsMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
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

    public void SettingsMenuOn()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        tutorial.tutorialEnabled = false;

        hud.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
        SortMenu();

        moveScript.enabled = false;

    }

    public void SettingsMenuOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        tutorial.tutorialEnabled = true;

        settingsMenu.gameObject.SetActive(false);
        hud.gameObject.SetActive(true);
        SortMenu();

        moveScript.enabled = true;

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
