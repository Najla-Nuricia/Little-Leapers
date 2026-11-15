
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    public InputActionAsset InputActions;

    private InputAction playerMoveAction;
    private InputAction playerJumpAction;

    [Header("Player")]
    public CharacterController playerCharacterController;
    public Transform playerCamera;

    [Header("Movement Settings")]
    public float playerWalkSpeed = 5.0f;
    public float playerRotateDampening = 0.1f;

    private float turnSmoothingVelocity;

    [Header("Jump Settings")]
    public float gravity = -9.8f;
    public float jumpHeight = 5.0f;
    public int maxJumps = 2;

    private float verticalVelocity = 0f;
    private int jumpsUsed = 0;

    private Vector2 playerMoveAmount;

    [Header("Dash Settings")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.5f;
    public float doubleTapTime = 0.3f;

    private float lastForwardTapTime = -1f;
    private float dashTimer = 0f;
    private float lastDashTime = -1f;

    private bool wasForwardPressed = false;
    private bool isDashing = false;

    private Vector3 dashDirection;

    private void Awake()
    {
        InputActionMap playerMap = InputActions.FindActionMap("Player");

        playerMoveAction = playerMap.FindAction("Move");
        playerJumpAction = playerMap.FindAction("Jump");
    }

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Update()
    {
        playerMoveAmount = playerMoveAction.ReadValue<Vector2>();

        DetectDoubleTapDash();

        Jump();

        PlayerMoveAndRotate();
    }

    // =========================
    // MOVEMENT
    // =========================

    private void PlayerMoveAndRotate()
    {
        Vector3 verticalMove = Vector3.up * verticalVelocity * Time.deltaTime;

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            playerCharacterController.Move(
                dashDirection * dashSpeed * Time.deltaTime
                + verticalMove
            );

            if (dashTimer <= 0f)
            {
                isDashing = false;
            }

            return;
        }

        Vector3 playerDirection = new Vector3(
            playerMoveAmount.x,
            0f,
            playerMoveAmount.y
        ).normalized;

        if (playerDirection.magnitude >= 0.1f)
        {
            float targetAngle =
                Mathf.Atan2(playerDirection.x, playerDirection.z)
                * Mathf.Rad2Deg
                + playerCamera.eulerAngles.y;

            float smoothTargetAngle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothingVelocity,
                playerRotateDampening
            );

            transform.rotation = Quaternion.Euler(
                0f,
                smoothTargetAngle,
                0f
            );

            Vector3 moveDirection =
                Quaternion.Euler(0f, targetAngle, 0f)
                * Vector3.forward;

            playerCharacterController.Move(
                moveDirection.normalized * playerWalkSpeed * Time.deltaTime
                + verticalMove
            );
        }
        else
        {
            playerCharacterController.Move(verticalMove);
        }
    }

    private void Jump()
    {
        if (playerCharacterController.isGrounded)
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = -1f;
            }

            jumpsUsed = 0;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        if (playerJumpAction.WasPressedThisFrame() && jumpsUsed < maxJumps)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

            jumpsUsed++;
        }
    }

    private void DetectDoubleTapDash()
    {
        bool forwardPressed = playerMoveAmount.y > 0.5f;

        // Detect W key press (rising edge)
        if (forwardPressed && !wasForwardPressed)
        {
            float currentTime = Time.time;

            if (currentTime - lastForwardTapTime <= doubleTapTime)
            {
                TryDash();
                lastForwardTapTime = -1f;
            }
            else
            {
                lastForwardTapTime = currentTime;
            }
        }

        wasForwardPressed = forwardPressed;
    }

    private void TryDash()
    {
        if (isDashing) return;

        if (Time.time - lastDashTime < dashCooldown) return;

        isDashing = true;
        dashTimer = dashDuration;
        lastDashTime = Time.time;

        // Dash according to player's facing direction
        dashDirection = transform.forward;
    }
}
