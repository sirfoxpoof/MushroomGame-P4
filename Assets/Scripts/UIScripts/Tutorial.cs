using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public bool tutorialEnabled, w, a, s, d, jump, glide, attack, finsihed;
    public Toggle wt, at, st, dt, jumpt, glidet, attackt;
    public GameObject tutorialmove, tutorialjump, tutorialglide, tutorialattack;


    public void W(InputAction.CallbackContext context)
    {
        if (tutorialEnabled)
        {
            w = true;
            wt.isOn = true;
            Invoke("CheckTutorial", 2);
        }
    }

    public void A(InputAction.CallbackContext context)
    {
        if (tutorialEnabled)
        {
            a = true;
            at.isOn = true;
            Invoke("CheckTutorial", 2);
        }
    }

    public void S(InputAction.CallbackContext context)
    {
        if (tutorialEnabled)
        {
            s = true;
            st.isOn = true;
            Invoke("CheckTutorial", 2);
        }
    }

    public void D(InputAction.CallbackContext context)
    {
        if (tutorialEnabled)
        {
            d = true;
            dt.isOn = true;
            Invoke("CheckTutorial", 2);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (tutorialEnabled)
        {
            if (a && s && w && d)
            {
                jump = true;
                jumpt.isOn = true;
                Invoke("CheckTutorial", 2);
            }
        }
    }

    public void Glide(InputAction.CallbackContext context)
    {
        if (tutorialEnabled)
        {
            if (jump)
            {
                glide = true;
                glidet.isOn = true;
                Invoke("CheckTutorial", 2);
            }
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (tutorialEnabled)
        {
            if (glide)
            {
                attack = true;
                attackt.isOn = true;
                Invoke("CheckTutorial", 2);
            }
        }
    }

    void CheckTutorial()
    {
        if (a && s && w && d)
        {
            tutorialmove.SetActive(false);
            tutorialjump.SetActive(true);
       
            if (jump)
            {
                tutorialjump.SetActive(false);
                tutorialglide.SetActive(true);
               
                if (glide)
                {
                    tutorialglide.SetActive(false);
                    tutorialattack.SetActive(true);
                  
                    if (attack)
                    {
                        tutorialattack.SetActive(false);
                        finsihed = true;
                        
                       
                    }
                }

            }
        }

    }

}

