
using UnityEngine;
using Unity.Cinemachine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Objects")]
    [SerializeField] private GameObject mainMenuObjects;

    [Header("In Game Objects")]
    [SerializeField] private GameObject inGameObjects;

    [Header("Cameras")]
    [SerializeField] private CinemachineCamera menuCamera;
    [SerializeField] private CinemachineCamera gameplayCamera;

    [Header("Player")]
    [SerializeField] private MonoBehaviour playerMovement;

    private void Start()
    {
        // Initial state
        mainMenuObjects.SetActive(true);
        inGameObjects.SetActive(false);

        menuCamera.Priority = 20;
        gameplayCamera.Priority = 10;

        // Disable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    public void StartGame()
    {
        // Switch UI
        mainMenuObjects.SetActive(false);
        inGameObjects.SetActive(true);

        // Switch camera
        menuCamera.Priority = 10;
        gameplayCamera.Priority = 20;

        // Enable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    public void BackToMenu()
    {
        // Switch UI
        mainMenuObjects.SetActive(true);
        inGameObjects.SetActive(false);

        // Switch camera
        menuCamera.Priority = 20;
        gameplayCamera.Priority = 10;

        // Disable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
