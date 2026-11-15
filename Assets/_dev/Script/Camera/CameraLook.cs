
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraLook : MonoBehaviour
{
    [Header("Cinemachine")]
    [SerializeField] private CinemachineCamera cinemachineCamera;

    [Header("Mouse Look")]
    [SerializeField] private float sensitivity = 0.15f;

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 0.001f;
    [SerializeField] private float minZoom = 0.5f;
    [SerializeField] private float maxZoom = 2.5f;

    private CinemachineOrbitalFollow orbitalFollow;

    private void Awake()
    {
        orbitalFollow = cinemachineCamera
            .GetComponent<CinemachineOrbitalFollow>();
    }

    private void Update()
    {
        if (Mouse.current == null || orbitalFollow == null)
            return;

        HandleMouseLook();
        HandleZoom();
    }

    private void HandleMouseLook()
    {
        if (!Mouse.current.rightButton.isPressed)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        orbitalFollow.HorizontalAxis.Value +=
            mouseDelta.x * sensitivity;

        orbitalFollow.VerticalAxis.Value -=
            mouseDelta.y * sensitivity;
    }

    private void HandleZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (Mathf.Abs(scroll) < 0.01f)
            return;

        float zoom = orbitalFollow.RadialAxis.Value;

        zoom -= scroll * zoomSpeed;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);

        orbitalFollow.RadialAxis.Value = zoom;
    }
}
