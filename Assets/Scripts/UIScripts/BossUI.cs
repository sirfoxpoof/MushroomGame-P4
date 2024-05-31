using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] GameObject bossUI;
    [SerializeField] AnimatorOff animatorOff;

    public void Update()
    {
        if (enemy.inSightRange == true)
        {
            print("suhfdiuefhgeiu");
            bossUI.SetActive(true);
            print("aaaaaaaaaaaaaa");

            if (animatorOff.animationFinished)
            {
                animatorOff.enabled = false;
            }
        }
    }
}
