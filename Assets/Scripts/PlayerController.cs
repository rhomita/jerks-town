using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Animator animator;

    private float speed = 11f;
    private float jumpHeight = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private bool isGrounded;

    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
        animator = transform.GetComponent<Animator>();
    }

    // TODO: Add run
    void Update()
    {
        CheckGround();
        AddGravity();

        GetInput();
        CalculateJump();
    }

    void FixedUpdate()
    {
        MakeMovement();
    }

    void AddGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }
        else
        {
            velocity.y += (Physics.gravity.y * 1.2f) * Time.deltaTime;
        }

        velocity.y = Mathf.Clamp(velocity.y, -10, 5);
    }

    void GetInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        velocity.x = horizontal;
        velocity.z = vertical;
    }

    private void CalculateJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }

    private void MakeMovement()
    {
        Vector3 forward = transform.forward.normalized;
        Vector3 right = transform.right.normalized;
        forward.y = 0;
        right.y = 0;
        Vector3 movement = forward * velocity.z + right * velocity.x;

        bool isRunning = Vector3.Magnitude(movement) > 0;
        animator.SetBool("isRunning", isRunning);

        movement.y = velocity.y;

        float _speed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed *= 1.7f;
        }
        controller.Move(movement * _speed * Time.fixedDeltaTime);
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);
    }
}
