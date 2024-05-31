using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] GameObject mushroom;

    public void Start()
    {
        mushroom = this.gameObject;
    }
    public void RotateLeft(int rotationValueLeft)
    {
        mushroom.transform.Rotate(0 , rotationValueLeft, 0);
    }

    public void RotateRight(int rotationValueRight)
    {
        mushroom.transform.Rotate(0, rotationValueRight, 0);


    }
}
