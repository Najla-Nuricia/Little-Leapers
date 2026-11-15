
using UnityEngine;
using TMPro;

public class GemCounter : MonoBehaviour
{
    public static GemCounter Instance;

    [Header("Gem UI")]
    [SerializeField] private TextMeshProUGUI gemText;

    private int gemCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        UpdateUI();
    }

    public void AddGem()
    {
        gemCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        gemText.text = gemCount.ToString();
    }
}
