using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSanityUI : MonoBehaviour
{
    [SerializeField] private Slider sanitySlider;
    [SerializeField] private Image playerEmotion;

    // Stored from least sane to most sane.
    [SerializeField] private List<Sprite> emotions;

    public void InitializeSanityBar(float maxSanity, float currentSanity)
    {
        sanitySlider.maxValue = maxSanity;
        sanitySlider.value = currentSanity;
    }
    
    public void UpdateSanityUI(float newValue)
    {
        sanitySlider.value = newValue;
    }

    public void UpdateEmotionUI(int currentEmotionIndex)
    {
        playerEmotion.sprite = emotions[currentEmotionIndex];
    }

    public int GetNumEmotions()
    {
        return emotions.Count;
    }
}
