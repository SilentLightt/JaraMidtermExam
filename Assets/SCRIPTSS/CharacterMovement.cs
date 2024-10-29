using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
//public class CharacterMovement : MonoBehaviour
//{
//    public StatModifier statModifier; // Reference to StatModifier for player stats
//    public CharacterAnimation characterAnimation; // Reference to the animation script
//    public Transform cameraTransform; // Reference to the camera for direction
//    public float gravity = -9.81f;
//    public float jumpHeight = 1f;
//    public float jumpDelay = 0.5f;
//    public float attackRange = 2.0f; // Maximum range for attacks

//    private CharacterController controller;
//    private Vector3 velocity;
//    private bool isJumping = false;
//    private bool jumpInitiated = false;
//    private bool isAttacking = false;

//    void Start()
//    {
//        controller = GetComponent<CharacterController>();
//    }

//    void Update()
//    {
//        HandleMovementInput();
//        ApplyGravity();
//        HandleAttackInput();
//    }

//    void HandleMovementInput()
//    {
//        float moveSpeed = statModifier.playerMovementSpeed;
//        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
//                         Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
//        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
//        bool jumpPressed = Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && !jumpInitiated;

//        if (jumpPressed)
//        {
//            jumpInitiated = true;
//            StartCoroutine(JumpWithDelay());
//        }

//        float speedMultiplier = isRunning ? 1.5f : 1f;
//        Vector3 moveDirection = Vector3.zero;

//        Vector3 forward = cameraTransform.forward;
//        Vector3 right = cameraTransform.right;

//        forward.y = 0;
//        right.y = 0;
//        forward.Normalize();
//        right.Normalize();

//        if (Input.GetKey(KeyCode.W)) moveDirection += forward;
//        if (Input.GetKey(KeyCode.S)) moveDirection -= forward;
//        if (Input.GetKey(KeyCode.D)) moveDirection += right;
//        if (Input.GetKey(KeyCode.A)) moveDirection -= right;

//        moveDirection.Normalize();

//        if (moveDirection != Vector3.zero)
//        {
//            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
//        }

//        controller.Move(moveDirection * (moveSpeed * speedMultiplier) * Time.deltaTime);

//        characterAnimation.UpdateAnimationStates(isWalking, isRunning, isJumping);
//    }

//    private IEnumerator JumpWithDelay()
//    {
//        characterAnimation.TriggerJump();
//        yield return new WaitForSeconds(jumpDelay);

//        isJumping = true;
//        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

//        jumpInitiated = false;
//    }

//    void ApplyGravity()
//    {
//        if (!controller.isGrounded)
//        {
//            velocity.y += gravity * Time.deltaTime;
//            controller.Move(velocity * Time.deltaTime);
//        }
//        else if (controller.isGrounded && isJumping)
//        {
//            isJumping = false;
//            velocity.y = 0f;
//        }
//    }

//    void HandleAttackInput()
//    {
//        if (Input.GetMouseButtonDown(0) && !isAttacking) // Left mouse button initiates attack
//        {
//            StartCoroutine(Attack());
//        }
//    }

//    private IEnumerator Attack()
//    {
//        isAttacking = true;
//        characterAnimation.TriggerAttackAnimation();

//        // Cast a ray to check if there is an enemy within attack range
//        RaycastHit hit;
//        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, attackRange))
//        {
//            Enemy enemy = hit.collider.GetComponent<Enemy>();
//            if (enemy != null)
//            {
//                // Use StatModifier's CalculateDamage to determine damage, crits, and misses
//                float damage = statModifier.CalculateDamage(enemy.playerStats, out bool isCritical, out bool isMissed);

//                if (isMissed)
//                {
//                    enemy.SpawnCombatText("Missed", Color.blue, hit.point);
//                }
//                else
//                {
//                    if (isCritical)
//                    {
//                        enemy.SpawnCombatText($"Critical! {damage:F1}", Color.red, hit.point);
//                    }
//                    else
//                    {
//                        enemy.SpawnCombatText($"{damage:F1}", Color.yellow, hit.point);
//                    }

//                    enemy.playerStats.health -= damage;
//                    enemy.playerStats.health = Mathf.Max(enemy.playerStats.health, 0);
//                    enemy.UpdateHealthUI(); // Update enemy health display
//                }
//            }
//        }

//        yield return new WaitForSeconds(1f / statModifier.attackSpeed); // Cooldown based on attack speed
//        isAttacking = false;
//    }


//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, attackRange);
//        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * attackRange, Color.red, 1f);

//    }
//}

public class CharacterMovement : MonoBehaviour
{
    public StatModifier statModifier; // Reference to StatModifier
    public CharacterAnimation characterAnimation; // Reference to the animation script
    public Transform cameraTransform; // Reference to the camera
    public float gravity = -9.81f;
    public float jumpHeight = 3f; // Set the jump height
    public float jumpDelay = 0.2f; // Delay before jumping

    private CharacterController controller;
    private Vector3 velocity;
    private bool isJumping = false;
    private bool jumpInitiated = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovementInput();
        ApplyGravity();
    }

    void HandleMovementInput()
    {
        // Check if character is walking or running
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                         Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && !jumpInitiated;

        // Start jump coroutine if space is pressed and the player is grounded
        if (jumpPressed)
        {
            jumpInitiated = true;
            StartCoroutine(JumpWithDelay());
        }

        // Adjust speed for running if shift is held
        float speedMultiplier = isRunning ? 1.5f : 1f;
        Vector3 moveDirection = Vector3.zero;

        // Get input and convert it relative to camera direction
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Flatten the forward and right vectors on the Y-axis
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Combine input with camera's direction
        if (Input.GetKey(KeyCode.W)) moveDirection += forward;
        if (Input.GetKey(KeyCode.S)) moveDirection -= forward;
        if (Input.GetKey(KeyCode.D)) moveDirection += right;
        if (Input.GetKey(KeyCode.A)) moveDirection -= right;

        moveDirection.Normalize();

        // Rotate the character to face the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Apply movement speed from StatModifier
        controller.Move(moveDirection * (statModifier.currentMovementSpeed * speedMultiplier) * Time.deltaTime);

        // Notify the animation script of movement states
        characterAnimation.UpdateAnimationStates(isWalking, isRunning, isJumping);
    }

    private IEnumerator JumpWithDelay()
    {
        // Trigger jump animation and wait for delay
        characterAnimation.TriggerJump();
        yield return new WaitForSeconds(jumpDelay);

        // Apply jump force
        isJumping = true;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Reset the jump initiation flag
        jumpInitiated = false;
    }

    void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        else if (controller.isGrounded && isJumping)
        {
            // Reset jump state when grounded
            isJumping = false;
            velocity.y = 0f;
        }
    }
}


//working jump and script
//public class CharacterMovement : MonoBehaviour
//{
//    public StatModifier statModifier; // Reference to StatModifier
//    public CharacterAnimation characterAnimation; // Reference to the animation script
//    public Transform cameraTransform; // Reference to the camera
//    public float gravity = -9.81f;
//    public float jumpHeight = 3f; // Set the jump height
//    public float jumpDelay = 0.2f; // Delay before jumping

//    private CharacterController controller;
//    private Vector3 velocity;
//    private bool isJumping = false;
//    private bool jumpInitiated = false;

//    void Start()
//    {
//        controller = GetComponent<CharacterController>();
//    }

//    void Update()
//    {
//        HandleMovementInput();
//        ApplyGravity();
//    }

//    void HandleMovementInput()
//    {
//       // float moveSpeed = statModifier.playerMovementSpeed;
//        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
//                         Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
//        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
//        bool jumpPressed = Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && !jumpInitiated;

//        // Start jump coroutine if space is pressed and the player is grounded
//        if (jumpPressed)
//        {
//            jumpInitiated = true;
//            StartCoroutine(JumpWithDelay());
//        }

//        // Adjust speed for running if shift is held
//        float speedMultiplier = isRunning ? 1.5f : 1f;
//        Vector3 moveDirection = Vector3.zero;

//        // Get input and convert it relative to camera direction
//        Vector3 forward = cameraTransform.forward;
//        Vector3 right = cameraTransform.right;

//        // Flatten the forward and right vectors on the Y-axis
//        forward.y = 0;
//        right.y = 0;
//        forward.Normalize();
//        right.Normalize();

//        // Combine input with camera's direction
//        if (Input.GetKey(KeyCode.W)) moveDirection += forward;
//        if (Input.GetKey(KeyCode.S)) moveDirection -= forward;
//        if (Input.GetKey(KeyCode.D)) moveDirection += right;
//        if (Input.GetKey(KeyCode.A)) moveDirection -= right;

//        moveDirection.Normalize();

//        // Rotate the character to face the movement direction
//        if (moveDirection != Vector3.zero)
//        {
//            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
//        }

//       controller.Move(moveDirection * (moveSpeed * speedMultiplier) * Time.deltaTime);

//        // Notify the animation script of movement states
//        characterAnimation.UpdateAnimationStates(isWalking, isRunning, isJumping);
//    }

//    private IEnumerator JumpWithDelay()
//    {
//        // Trigger jump animation and wait for delay
//        characterAnimation.TriggerJump();
//        yield return new WaitForSeconds(jumpDelay);

//        // Apply jump force
//        isJumping = true;
//        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

//        // Reset the jump initiation flag
//        jumpInitiated = false;
//    }

//    void ApplyGravity()
//    {
//        if (!controller.isGrounded)
//        {
//            velocity.y += gravity * Time.deltaTime;
//            controller.Move(velocity * Time.deltaTime);
//        }
//        else if (controller.isGrounded && isJumping)
//        {
//            // Reset jump state when grounded
//            isJumping = false;
//            velocity.y = 0f;
//        }
//    }
//}

//instant jump script
//public class CharacterMovement : MonoBehaviour
//{
//    public StatModifier statModifier; // Reference to StatModifier
//    public CharacterAnimation characterAnimation; // Reference to the animation script
//    public Transform cameraTransform; // Reference to the camera
//    public float gravity = -9.81f;
//    public float jumpHeight = 3f; // Set the jump height

//    private CharacterController controller;
//    private Vector3 velocity;
//    private bool isJumping = false;

//    void Start()
//    {
//        controller = GetComponent<CharacterController>();
//    }

//    void Update()
//    {
//        HandleMovementInput();
//        ApplyGravity();
//    }

//    void HandleMovementInput()
//    {
//        float moveSpeed = statModifier.playerMovementSpeed;
//        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
//                         Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
//        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
//        bool jumpPressed = Input.GetKeyDown(KeyCode.Space) && controller.isGrounded;

//        // Trigger jump if space is pressed and the player is grounded
//        if (jumpPressed)
//        {
//            isJumping = true;
//            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump velocity
//            characterAnimation.TriggerJump();
//        }

//        // Adjust speed for running if shift is held
//        float speedMultiplier = isRunning ? 1.5f : 1f;
//        Vector3 moveDirection = Vector3.zero;

//        // Get input and convert it relative to camera direction
//        Vector3 forward = cameraTransform.forward;
//        Vector3 right = cameraTransform.right;

//        // Flatten the forward and right vectors on the Y-axis
//        forward.y = 0;
//        right.y = 0;
//        forward.Normalize();
//        right.Normalize();

//        // Combine input with camera's direction
//        if (Input.GetKey(KeyCode.W)) moveDirection += forward;
//        if (Input.GetKey(KeyCode.S)) moveDirection -= forward;
//        if (Input.GetKey(KeyCode.D)) moveDirection += right;
//        if (Input.GetKey(KeyCode.A)) moveDirection -= right;

//        moveDirection.Normalize();

//        // Rotate the character to face the movement direction
//        if (moveDirection != Vector3.zero)
//        {
//            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
//        }

//        controller.Move(moveDirection * (moveSpeed * speedMultiplier) * Time.deltaTime);

//        // Notify the animation script of movement states
//        characterAnimation.UpdateAnimationStates(isWalking, isRunning, isJumping);
//    }

//    void ApplyGravity()
//    {
//        if (!controller.isGrounded)
//        {
//            velocity.y += gravity * Time.deltaTime;
//            controller.Move(velocity * Time.deltaTime);
//        }
//        else if (controller.isGrounded && isJumping)
//        {
//            // Reset jump state when grounded
//            isJumping = false;
//            velocity.y = 0f;
//        }
//    }
//}
//public class CharacterMovement : MonoBehaviour
//{
//    public StatModifier playerStats;
//    public Rigidbody rb;  // Reference to the Rigidbody for movement
//    public float movementSpeedMultiplier = 1.0f;  // Speed multiplier

//    private Vector3 movementInput;
//    private bool isAttacking = false;
//    private AnimationHandler animationHandler;  // Reference to AnimationHandler

//    private void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        animationHandler = GetComponent<AnimationHandler>();
//        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Prevent tilting
//    }

//    private void Update()
//    {
//        HandleMovementInput();
//        HandleAttackInput();
//    }

//    private void FixedUpdate()
//    {
//        MovePlayer();
//    }

//    private void HandleMovementInput()
//    {
//        // Get input for movement
//        float moveHorizontal = Input.GetAxis("Horizontal");  // A and D keys (left and right)
//        float moveVertical = Input.GetAxis("Vertical");  // W and S keys (forward and backward)

//        // Adjust speed based on player's stats
//        float speed = playerStats.playerMovementSpeed + movementSpeedMultiplier;

//        // Create movement vector based on input and normalize it
//        movementInput = new Vector3(moveHorizontal, 0f, moveVertical).normalized * speed;

//        // Determine movement states
//        bool isWalking = movementInput.magnitude > 0;
//        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);

//        // Call animationHandler to manage animations
//        animationHandler.SetMovementState(isWalking, isRunning);

//        // Rotate towards movement direction (if there's any movement input)
//        if (movementInput.magnitude > 0.1f)
//        {
//            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
//            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 0.1f);  // Smooth rotation
//        }
//    }

//    private void HandleAttackInput()
//    {
//        if (!isAttacking && Input.GetMouseButtonDown(0)) // Left mouse click for attack
//        {
//            StartCoroutine(Attack());
//        }
//    }

//    private IEnumerator Attack()
//    {
//        isAttacking = true;

//        // Trigger attack animation
//        animationHandler.TriggerAttack();

//        // Delay based on attack speed
//        float attackCooldown = 1f / playerStats.attackSpeed;
//        yield return new WaitForSeconds(attackCooldown);

//        isAttacking = false;
//    }

//    private void MovePlayer()
//    {
//        // Retain existing vertical velocity (to avoid overriding gravity effects)
//        Vector3 currentVelocity = rb.velocity;
//        float verticalVelocity = currentVelocity.y;

//        // Apply movement while preserving vertical velocity
//        rb.velocity = new Vector3(movementInput.x, verticalVelocity, movementInput.z);
//    }
//}

