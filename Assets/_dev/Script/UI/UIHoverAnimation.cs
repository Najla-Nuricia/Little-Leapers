
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIHoverAnimation : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover Animation")]
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private Ease ease = Ease.OutBack;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();

        transform.DOScale(originalScale * hoverScale, duration)
            .SetEase(ease);

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayHoverSFX();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();

        transform.DOScale(originalScale, duration)
            .SetEase(ease);
    }

    private void OnDisable()
    {
        transform.DOKill();
        transform.localScale = originalScale;
    }
}
