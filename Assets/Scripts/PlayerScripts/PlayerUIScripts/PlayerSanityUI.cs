using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSanityUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider sanitySlider;
    [SerializeField] private Image playerEmotion;

    [Header("Emotion Sprites")]
    [SerializeField] private List<Sprite> emotions1;
    [SerializeField] private List<Sprite> emotions2;

    [Header("Emotion Settings")]
    [SerializeField, Tooltip("Base time between emotion swaps (seconds)")]
    private float baseSwapInterval = 1.5f;

    [SerializeField, Tooltip("How much faster swapping gets at 0 sanity (multiplier)")]
    private float swapSpeedMultiplier = 3f;

    [SerializeField, Tooltip("Maximum shake intensity at 0 sanity")]
    private float maxShakeAmount = 5f;

    private int currentEmotionIndex = 0;
    private bool useFirstSet = true;
    private float swapTimer = 0f;
    private float sanityPercent = 1f; // 1 = full sanity, 0 = insane

    private Vector3 originalPos;

    private void Start()
    {
        if (playerEmotion != null)
            originalPos = playerEmotion.rectTransform.localPosition;
    }

    private void Update()
    {
        if (!playerEmotion) return;

        float currentSwapInterval = Mathf.Lerp(baseSwapInterval / swapSpeedMultiplier,
            baseSwapInterval, sanityPercent);

        swapTimer += Time.deltaTime;
        if (swapTimer >= currentSwapInterval)
        {
            swapTimer = 0f;
            useFirstSet = !useFirstSet;
            UpdateDisplayedEmotion();
        }

        ApplyShake();
    }

    public void InitializeSanityBar(float maxSanity, float currentSanity)
    {
        sanitySlider.maxValue = maxSanity;
        sanitySlider.value = currentSanity;
        sanityPercent = 1f;
    }

    public void UpdateSanityUI(float newValue)
    {
        sanitySlider.value = newValue;
        sanityPercent = sanitySlider.value / sanitySlider.maxValue;
    }

    public void UpdateEmotionUI(int index)
    {
        currentEmotionIndex = Mathf.Clamp(index, 0, emotions1.Count - 1);
        UpdateDisplayedEmotion();
    }

    private void UpdateDisplayedEmotion()
    {
        if (useFirstSet && emotions1.Count > currentEmotionIndex)
            playerEmotion.sprite = emotions1[currentEmotionIndex];
        else if (emotions2.Count > currentEmotionIndex)
            playerEmotion.sprite = emotions2[currentEmotionIndex];
    }

    private void ApplyShake()
    {
        float shakeAmount = Mathf.Lerp(0f, maxShakeAmount, 1f - sanityPercent);
        Vector2 shakeOffset = Random.insideUnitCircle * shakeAmount;
        playerEmotion.rectTransform.localPosition = originalPos + (Vector3)shakeOffset;
    }

    public int GetNumEmotions()
    {
        return Mathf.Min(emotions1.Count, emotions2.Count);
    }
}
