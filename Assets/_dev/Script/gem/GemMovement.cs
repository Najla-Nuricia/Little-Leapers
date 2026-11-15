
using UnityEngine;
using DG.Tweening;

public class GemMovement : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 360f;

    [Header("Floating")]
    [SerializeField] private float floatHeight = 0.5f;
    [SerializeField] private float floatDuration = 1f;

    private Vector3 startPosition;

    private Tween floatTween;
    private Tween rotateTween;

    private void Start()
    {
        startPosition = transform.position;

        // Floating up and down
        floatTween = transform.DOMoveY(
            startPosition.y + floatHeight,
            floatDuration
        )
        .SetEase(Ease.InOutSine)
        .SetLoops(-1, LoopType.Yoyo);

        // Rotate continuously
        rotateTween = transform.DORotate(
            new Vector3(0f, 360f, 0f),
            360f / rotationSpeed,
            RotateMode.FastBeyond360
        )
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart);
    }

    private void OnDestroy()
    {
        floatTween?.Kill();
        rotateTween?.Kill();
    }
}
