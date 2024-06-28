using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MausHead : MonoBehaviour
{
    public GameObject newParentObject, oldSword;
    
    bool inRange, pickedup;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
    public void PickUpSword(InputAction.CallbackContext context)
    {

        if (context.performed && inRange && !pickedup)
        {
            newParentObject.SetActive(true);
            pickedup = true;
            oldSword.SetActive(false);

            gameObject.SetActive(false);
        }
    }
}
