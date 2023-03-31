using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Handling")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float sprintSpeed = 3.0f;

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
    private float savedSpeed;
    private bool isSprinting = false;

    // Game Handling:
    private bool canMove = true;

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public bool CanSprint
    {
        get { return canSprint; }
        set { canSprint = value; }
    }

    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }

    public bool IsSprinting
    {
        get { return isSprinting; } 
        set { isSprinting = value; }
    }

    void Start()
    {
        old_pos = transform.position.x;
        savedSpeed = moveSpeed;

        characterController = GetComponent<CharacterController>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (canMove)
        {
            Movement();
            GravityChecking();
            CheckIfMoving();
            if (canSprint)
            {
                Sprinting();
            }
        }
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
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementZ = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * movementX + transform.forward * movementZ;
        characterController.Move(move.normalized * moveSpeed * Time.deltaTime);
    }

    private void CheckIfMoving()
    {
        if (old_pos != transform.position.x)
        {
            isMoving = true;
        }
        else
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

    private void Sprinting()
    {
        if (isMoving)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = sprintSpeed;
                isSprinting = true;
                return;
            }
            moveSpeed = savedSpeed;
            isSprinting = false;
        }
        moveSpeed = savedSpeed;
        return;
    }

    public void RevertNormalSpeed()
    {
        moveSpeed = savedSpeed;
    }
}
