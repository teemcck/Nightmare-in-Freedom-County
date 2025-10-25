using System.Collections.Generic;
using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [SerializeField] PlayerSanityUI sanityUI;
    [SerializeField] private float sanityLossPerSecond = 0.25f;
    private readonly float maxSanity = 100f;
    private float currentSanity;
    private int currentEmotionIndex;
    private float emotionIncrement;

    void Update()
    {
        ModifySanity(-sanityLossPerSecond * Time.deltaTime);
    }

    void Start()
    {
        currentSanity = maxSanity;
        currentEmotionIndex = sanityUI.GetNumEmotions() - 1;
        sanityUI.InitializeSanityBar(maxSanity, currentSanity);
        emotionIncrement = maxSanity / sanityUI.GetNumEmotions();
    }

    public void ModifySanity(float amount)
    {
        currentSanity = Mathf.Clamp(currentSanity + amount, 0, maxSanity);
        sanityUI.UpdateSanityUI(currentSanity);
        // Updates face to the side of sanity bar.
        TryUpdateEmotion();
    }

    public void ResetSanity()
    {
        currentSanity = maxSanity;
        sanityUI.UpdateSanityUI(currentSanity);
    }

    private void TryUpdateEmotion()
    {
        int newEmotionIndex = Mathf.FloorToInt((currentSanity / maxSanity) 
            * (sanityUI.GetNumEmotions() - 1));

        if (currentEmotionIndex != newEmotionIndex)
        {
            currentEmotionIndex = newEmotionIndex;
            sanityUI.UpdateEmotionUI(currentEmotionIndex);
        }
    }
}