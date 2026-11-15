
using UnityEngine;

public class GemCollect : MonoBehaviour
{
    [Header("Sound Effect")]
    [SerializeField] private AudioClip collectSFX;
    [SerializeField] private float sfxVolume = 1f;

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
            if (collectSFX != null)
            {
                AudioSource.PlayClipAtPoint(
                    collectSFX,
                    transform.position,
                    sfxVolume
                );
            }

            // Destroy gem
            Destroy(gameObject);
        }
    }
}
