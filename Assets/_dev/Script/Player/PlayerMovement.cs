
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

    private float verticalVelocity = 0f;
    private Vector2 playerMoveAmount;

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

        PlayerMoveAndRotate();
        Jump();
    }

    private void PlayerMoveAndRotate()
    {
        Vector3 playerDirection = new Vector3(
            playerMoveAmount.x,
            0f,
            playerMoveAmount.y
        ).normalized;

        Vector3 verticalMove = new Vector3(
            0f,
            verticalVelocity,
            0f
        ) * Time.deltaTime;

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
            verticalVelocity = -1f;

            if (playerJumpAction.WasPressedThisFrame())
            {
                verticalVelocity = jumpHeight;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }
}
