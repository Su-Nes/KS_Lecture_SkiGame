using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;
    private Animator animator;
    private bool hasJumped;
    private TakeDamage playerTakeDamage;
    
    [SerializeField] private float acceleration = 4f;
    [SerializeField] private float deceleration = -20f;
    private float accelMax;
    [SerializeField] private float turnSpeed = 4f;
    [SerializeField] private float boostSpeed = 3f;
    private float currentSpeed = 10f;
    private float moveInput;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerTakeDamage = GetComponent<TakeDamage>();
        
        accelMax = acceleration;
    }

    private void OnEnable()
    {
        PlayerEvents.boost += Boost;
    }

    private void OnDisable()
    {
        PlayerEvents.boost -= Boost;
    }

    private void Update()
    {
        bool isGrounded = Physics.Linecast(transform.position, groundCheck.position, groundLayer); // check for ground
        
        if (isGrounded)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            animator.SetBool("grounded", true);
            hasJumped = false;
        }
        else
        {
            if (!hasJumped)
            {
                animator.SetBool("grounded", false);
                hasJumped = true;
            }
            else
            {
                animator.SetBool("grounded", true);
            }
            moveInput = 0;
        }

        float angleDiffer = Mathf.Abs(180 - transform.rotation.eulerAngles.y);
        acceleration = Remap(angleDiffer, 90f, 0, deceleration, accelMax);
    }

    private void FixedUpdate()
    {
        if (playerTakeDamage.StunTime > 0f)
            return;
        
        transform.Rotate(transform.up, moveInput * turnSpeed * Time.fixedDeltaTime);
        LimitRotation();
        
        currentSpeed += acceleration * Time.fixedDeltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, -10f, 99999f); // infinite speed!
        animator.SetFloat("playerSpeed", rb.velocity.magnitude);
        
        Vector3 velocity = transform.forward * (currentSpeed * Time.fixedDeltaTime);
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private void Boost()
    {
        currentSpeed += boostSpeed;
    }

    private void LimitRotation()
    {
        if (transform.localRotation.eulerAngles.y < 89f)
            transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        if (transform.localRotation.eulerAngles.y > 271f)
            transform.localRotation = Quaternion.Euler(0f, 270f, 0f);
    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
