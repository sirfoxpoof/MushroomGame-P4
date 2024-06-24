using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public bool w, a, s, d, jump, glide, attack;
    public Toggle wt, at, st, dt, jumpt, glidet, attackt;
    public GameObject tutorialmove;

    public void W(InputAction.CallbackContext context)
    {
        w = true;
        wt.isOn = true;
        Invoke("CheckTutorial", 2);

    }
    public void A(InputAction.CallbackContext context)
    {
        a = true;
        at.isOn = true;
        Invoke("CheckTutorial", 2);

    }
    public void S(InputAction.CallbackContext context)
    {
        s = true;
        st.isOn = true;
        Invoke("CheckTutorial", 2);

    }
    public void D(InputAction.CallbackContext context)
    {
        d = true;
        dt.isOn = true;
        Invoke("CheckTutorial", 2);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        jump = true;
        jumpt.isOn = true;
        Invoke("CheckTutorial", 2);
    }



    void CheckTutorial()
    {
        if (a && s && w && d)
        {
            tutorialmove.SetActive(false);
            if (jump)
            {

            }
        }

    }

}

