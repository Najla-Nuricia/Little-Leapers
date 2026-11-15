
using UnityEngine;

public class GemCollect : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        if (other.CompareTag("Player"))
        {
            isCollected = true;

            // Add gem counter
            if (GemCounter.Instance != null)
            {
                GemCounter.Instance.AddGem();
            }

            // Play collection sound
            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.PlayGemSFX();
            }

            // Destroy gem
            Destroy(gameObject);
        }
    }
}
