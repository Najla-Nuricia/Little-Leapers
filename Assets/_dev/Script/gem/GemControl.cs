using UnityEngine;

public class GemRotator : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 100f;

    [Header("Sound Effect")]
    [SerializeField] private AudioClip collectSFX;
    [SerializeField] private float sfxVolume = 1f;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectSFX != null)
            {
                AudioSource.PlayClipAtPoint(
                    collectSFX,
                    transform.position,
                    sfxVolume
                );
            }

            Destroy(gameObject);
        }
    }
}