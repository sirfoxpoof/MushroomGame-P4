using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    private PlayerActionMap playerActionMap;
    private InputAction move;


    [HideInInspector] public Rigidbody rb;
    [Header("Values")]
    [SerializeField] float moveSpeed, normalSpeed;
    [SerializeField] float maxSpeed;
    public float jumpForce;
    [SerializeField] float maxJumpforce;
    /*[SerializeField] float boostForce;*/
    [HideInInspector] public Vector3 forceDirection;
    [HideInInspector] public Vector3 jump;

    [Header("Other Things")]
    [SerializeField] private Camera cam;
    /*[HideInInspector]*/ public bool isGrounded, jumping, gliding, notJumpable;
    public Animator movementAnimator;

    public int jumpCount = 0;
    private int jumpLimit = 1   ;

    RaycastHit hit;
    private void Awake()
    {
        playerActionMap = new PlayerActionMap();

        jump = new Vector3(0, 7, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        normalSpeed = moveSpeed;

        //miss hier alle stats van de player aangeven ofzo
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
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(cam) * moveSpeed;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(cam) * moveSpeed;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;


        if (rb.velocity.y < 0)
        {
            if (gliding)
            {
                rb.velocity -= Vector3.down * Physics.gravity.y * 0.1f * Time.fixedDeltaTime;

            }
            else
            {
                rb.velocity -= Vector3.down * Physics.gravity.y * 3.5f * Time.fixedDeltaTime;
            }
        }
        //als ik wil dat je soort van kunt gliden ofzo dan kun je net zoeits doen ^^

        Vector3 horizontalVel = rb.velocity;
        horizontalVel.y = 0;
        if (horizontalVel.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVel.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        //LayerMask layerMask = 1 << 9;
        if (Physics.Raycast(transform.position,-Vector3.up, out hit, 0.1f))
        {
            if (hit.transform.tag != "Jumpable")
            {
                return;
            }
            isGrounded = true;
            jumpCount = 0;
            gliding = false;
            jumping = false;
        }
        else
        {
            isGrounded = false;
        }

        float actualSpeed = new Vector3(rb.velocity.x, rb.velocity.z, 0).magnitude;
        movementAnimator.SetFloat("Actual Speed", actualSpeed);


        LookAt();
    }

    public void Gliding(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (gliding)
            {
                movementAnimator.SetTrigger("Glide done");
                gliding = false;

            }
            else
            {
                movementAnimator.SetTrigger("Glide");
                gliding = true;
                moveSpeed += 2;
            }
        }
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

    //hier kijken we in de directie die we opgaan
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

    public void DoJump(InputAction.CallbackContext context)
    {
        if(isGrounded)
        {
            //first jump
            if(context.performed)
            {
                jumpCount++;
                movementAnimator.SetTrigger("Jumping");

                jumping = true;
                isGrounded = false;
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                
            }
        }
        else
        {
            //double jump
            if(jumpCount < jumpLimit)
            {
                if (context.performed)
                {
                   jumpCount++;
                   rb.AddForce(jump * jumpForce * maxJumpforce, ForceMode.Impulse);
                   jumping = false;

                }
            }
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            maxSpeed *= 2;

            moveSpeed *= 2;
        }
        else
        {
            maxSpeed = 4;
            moveSpeed = normalSpeed;
        }
    }
}