using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOff : MonoBehaviour
{
    public bool animationFinished;

    private void Update()
    {
        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Klaar"))
        {
            this.GetComponent<Animator>().enabled = false;
            // Avoid any reload.
            animationFinished = true;
        }
    }

}

