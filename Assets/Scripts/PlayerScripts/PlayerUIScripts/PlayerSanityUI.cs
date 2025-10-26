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
    [SerializeField] private List<Sprite> emotions3;

    [SerializeField] private List<List<Sprite>> emotions;

    [SerializeField] private float baseSwapInterval = 0.2f;

    [SerializeField] private float swapSpeedMultiplier = 3f;

    [SerializeField] private float maxShakeAmount = 7.5f;

    private int currentEmotionIndex = 0;
    private int currentSubEmotionIndex = 0;
    private float swapTimer = 0f;
    private float sanityPercent = 1f;

    private Vector3 originalPos;

    void Awake()
    {
        emotions = new List<List<Sprite>>
        {
            emotions1,
            emotions2,
            emotions3
        };
    }

    private void Start()
    {
        if (playerEmotion != null)
        {
            originalPos = playerEmotion.rectTransform.localPosition;
        }
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
            currentSubEmotionIndex = (currentSubEmotionIndex + 1) % emotions[currentEmotionIndex].Count;

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
        currentEmotionIndex = Mathf.Clamp(index, 0, emotions.Count - 1);
        UpdateDisplayedEmotion();
    }

    private void UpdateDisplayedEmotion()
    {
        playerEmotion.sprite = emotions[currentEmotionIndex][currentSubEmotionIndex];
    }

    private void ApplyShake()
    {
        float shakeAmount = Mathf.Lerp(0f, maxShakeAmount, 1f - sanityPercent);
        Vector2 shakeOffset = Random.insideUnitCircle * shakeAmount;
        playerEmotion.rectTransform.localPosition = originalPos + (Vector3)shakeOffset;
    }

    public int GetNumEmotions()
    {
        return emotions.Count;
    }
}
