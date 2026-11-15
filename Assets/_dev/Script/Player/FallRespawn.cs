
using UnityEngine;

public class FallRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private float fallThreshold = -10f;

    private PlayerCheckpoint playerCheckpoint;

    private void Start()
    {
        playerCheckpoint = GetComponent<PlayerCheckpoint>();

        if (playerCheckpoint == null)
        {
            Debug.LogError("PlayerCheckpoint belum dipasang!");
        }
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
        if (playerCheckpoint != null)
        {
            playerCheckpoint.Respawn();
        }
    }
}
