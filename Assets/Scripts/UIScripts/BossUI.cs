using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BossUI : MonoBehaviour
{
    [SerializeField] Maus enemy;
    [SerializeField] GameObject bossUI;
    [SerializeField] AnimatorOff animatorOff;
    [SerializeField] AudioSource animalcrossing, eldenring;

    bool animatorIsOff = false;

    public void Update()
    {
        if (enemy.inSightRange == true)
        {
        
            bossUI.SetActive(true);
            

            if (!animatorIsOff)
            {
                animalcrossing.Pause();
                eldenring.Play();

                if (animatorOff.animationFinished)
                {
                    animatorIsOff = true;
                    animatorOff.enabled = false;
                    
                }
            }
        }
        else
        {
            bossUI.SetActive(false);
            animalcrossing.Play();
            eldenring.Pause();

        }
    }

}
