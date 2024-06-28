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
            transform.SetParent(newParentObject.transform);
            pickedup = true;
            oldSword.SetActive(false);

            /*transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;*/

            gameObject.GetComponent<Transform>().position = newParentObject.transform.position;
            gameObject.GetComponent<Transform>().rotation = newParentObject.transform.rotation;
            gameObject.GetComponent<Transform>().localScale = newParentObject.transform.localScale;
        }
    }
}
