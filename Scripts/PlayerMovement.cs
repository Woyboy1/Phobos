using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Handling")]
    [SerializeField] private float moveSpeed = 6;

    [Header("Gravity Handling")]
    [SerializeField] private float gravity = -9.81f; // default in the project settings
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private AudioSource source;
    private bool isMoving = false;
    private float old_pos;

    void Start()
    {
        old_pos = transform.position.x;

        characterController = GetComponent<CharacterController>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
        GravityChecking();
        CheckIfMoving();
        
    }

    private void GravityChecking()
    {
        // Gravity:
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Movement()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * movementX + transform.forward * movementZ;
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    private void CheckIfMoving()
    {
        if (old_pos != transform.position.x)
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }
        old_pos = transform.position.x;


        if (isMoving)
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        else
        {
            source.Stop();
        }
    }
}
