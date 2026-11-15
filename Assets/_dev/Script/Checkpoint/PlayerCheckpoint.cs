
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    private Vector3 checkpointPosition;
    private Quaternion checkpointRotation;

    private CharacterController characterController;
    private Rigidbody rb;

    private void Start()
    {
        checkpointPosition = transform.position;
        checkpointRotation = transform.rotation;

        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        checkpointPosition = checkpoint.position;
        checkpointRotation = checkpoint.rotation;

        Debug.Log("Checkpoint tersimpan: " + checkpointPosition);
    }

    public void Respawn()
    {
        if (characterController != null)
            characterController.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        transform.SetPositionAndRotation(
            checkpointPosition,
            checkpointRotation
        );

        if (characterController != null)
            characterController.enabled = true;

        Debug.Log("Respawn ke checkpoint!");
    }
}
