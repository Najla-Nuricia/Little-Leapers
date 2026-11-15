
using UnityEngine;

public class FallRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private float fallThreshold = -10f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private CharacterController characterController;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        if (characterController != null)
        {
            characterController.enabled = false;
        }

        transform.position = startPosition;
        transform.rotation = startRotation;

        if (characterController != null)
        {
            characterController.enabled = true;
        }
    }
}
