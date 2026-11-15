
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            PlayerCheckpoint player =
                other.GetComponent<PlayerCheckpoint>();

            if (player != null)
            {
                player.SetCheckpoint(transform);
                activated = true;

                Debug.Log("Checkpoint Activated!");
            }
        }
    }
}
