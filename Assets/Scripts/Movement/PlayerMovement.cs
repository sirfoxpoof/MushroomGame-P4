using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    private PlayerActionMap playerActionMap;
    private InputAction move;


    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveForce, jumpForce, maxSpeed;
    private Vector3 forceDirection, jump;

    [SerializeField] private Camera cam;

    public bool isGrounded;

    private void Awake()
    {
        playerActionMap = new PlayerActionMap();

        jump = new Vector3(0, 5, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        playerActionMap.Character.Jump.started += DoJump; 
        move = playerActionMap.Character.Move;
        playerActionMap.Character.Enable();
    }

    private void OnDisable()
    {
        playerActionMap.Character.Jump.started -= DoJump;
        playerActionMap.Character.Disable();
    }

    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(cam) * moveForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(cam) * moveForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;


        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
        }
        //als ik wil dat je soort van kunt gliden ofzo dan kun je net zoeits doen ^^

        Vector3 horizontalVel = rb.velocity;
        horizontalVel.y = 0;
        if (horizontalVel.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVel.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }


        LookAt();
    }

    private Vector3 GetCameraForward(Camera cam)
    {
        Vector3 forward = cam.transform.forward;
        forward.y = 0f;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera cam)
    {
        Vector3 right = cam.transform.right;
        right.y = 0f;
        return right.normalized;
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if(move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
           this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }


    void OnCollisionStay()
    {
        isGrounded = true;
    }
    public void DoJump(InputAction.CallbackContext context)
    {

        if(context.performed)
        {
            if (isGrounded)
            {

                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }
      
    }

  
}