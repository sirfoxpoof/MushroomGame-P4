using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] GameObject bossUI;
    [SerializeField] AnimatorOff animatorOff;

    bool animatorIsOff = false;

    public void Update()
    {
        if (enemy.inSightRange == true)
        {
            bossUI.SetActive(true);

            if (!animatorIsOff)
            {
                if (animatorOff.animationFinished)
                {
                    animatorIsOff = true;
                    animatorOff.enabled = false;
                }
            }
        }
    }
}
