
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject congratsCanvas;

    private bool isFinished;

    private void Start()
    {
        congratsCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFinished) return;

        if (other.CompareTag("Player"))
        {
            isFinished = true;

            congratsCanvas.SetActive(true);

            // Menghentikan gameplay
            Time.timeScale = 0f;
        }
    }
}
